using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minigis_Surkov
{
    public partial class LayerControl : UserControl
    {
        public Map map;
        public LayerControl()
        {
            InitializeComponent();
        }

        public void refreshList ()
        {
            if (map == null) { return; }

            listView1.BeginUpdate();

            listView1.Clear();
            foreach (Layer layer in map.layers)
            {
                ListViewItem item = new ListViewItem();
                item.Text = layer.name;
                item.Tag = layer;
                item.Checked = layer.isVisible;
                listView1.Items.Insert(0, item);
            }
            listView1.EndUpdate();
        }

        public void syncList ()
        {
            if (map == null) { return; }

            var temp = new List<Layer>();

            for (int i = listView1.Items.Count-1; i >= 0; i--)
            {
                ListViewItem item = listView1.Items[i];
                var tag = item.Tag as Layer;
                temp.Add(tag);
            }

            map.layers = temp;
            Refresh();
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (map  == null) { return; }
            Layer layer = e.Item.Tag as Layer;
            layer.isVisible = e.Item.Checked;
            map.Refresh();
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (sender != listView1) { return; }
            listView1.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void listView1_DragLeave(object sender, EventArgs e)
        {
            if (sender != listView1) { return; }
            listView1.InsertionMark.Index = -1;
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (sender != listView1) { return; }
            e.Effect = e.AllowedEffect;
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            if (sender != listView1) { return; }

            int targetIndex = listView1.InsertionMark.Index;
            if (targetIndex == -1) { return; }
            if (listView1.InsertionMark.AppearsAfterItem) { targetIndex++; }

            ListViewItem dragged = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

            Map old = map;
            map = null;
            listView1.Items.Insert(targetIndex, (ListViewItem)dragged.Clone());
            map = old;
            listView1.Items.Remove(dragged);
            
            syncList();
            refreshList();
        }

        private void listView1_DragOver(object sender, DragEventArgs e)
        {
            if (sender != listView1) { return; }

            System.Drawing.Point targetPoint = listView1.PointToClient(new System.Drawing.Point(e.X, e.Y));
            int targetIndex = listView1.InsertionMark.NearestIndex(targetPoint);

            if (targetIndex > -1) { 
                Rectangle itemBounds = listView1.GetItemRect(targetIndex);

                if (targetPoint.X > itemBounds.Left + (itemBounds.Width / 2))
                {
                    listView1.InsertionMark.AppearsAfterItem = true;
                } else
                {
                    listView1.InsertionMark.AppearsAfterItem = false;
                }
            }

            listView1.InsertionMark.Index = targetIndex;
        }
    }
}
