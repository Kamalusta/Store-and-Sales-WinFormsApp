using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace yeni_proyekt
{
    public class sirala : IComparer
    {

        public int Column { get; set; }
        //sıralama
        public SortOrder Order { get; set; }
        public sirala(int colIndex)
        {
            Column = colIndex;
            Order = SortOrder.None;

        }
        public int Compare(object a, object b)
        {
            int result;

            ListViewItem itemA = a as ListViewItem;
            ListViewItem itemB = b as ListViewItem;
            if (itemA == null && itemB == null)
                result = 0;
            else if (itemA == null)
                result = -1;
            else if (itemB == null)
                result = 1;

            if (itemA == itemB)
                result = 0;
            ///http://www.bilisimogretmeni.com/
            // tarihler karşılaştırılıyor
            DateTime x1, y1;

            if (!DateTime.TryParse(itemA.SubItems[Column].Text, out x1))
                x1 = DateTime.MinValue;
            if (!DateTime.TryParse(itemB.SubItems[Column].Text, out y1))
                y1 = DateTime.MinValue;
            result = DateTime.Compare(x1, y1);

            if (x1 != DateTime.MinValue && y1 != DateTime.MinValue)
                goto done;
            ///http://www.bilisimogretmeni.com/
            //sayılar karşılaştırılıyor
            decimal x2, y2;
            if (!Decimal.TryParse(itemA.SubItems[Column].Text, out x2))
                x2 = Decimal.MinValue;
            if (!Decimal.TryParse(itemB.SubItems[Column].Text, out y2))
                y2 = Decimal.MinValue;
            result = Decimal.Compare(x2, y2);

            if (x2 != Decimal.MinValue && y2 != Decimal.MinValue)
                goto done;
            ///http://www.bilisimogretmeni.com/
            //alfabetik sıralama
            result = String.Compare(itemA.SubItems[Column].Text, itemB.SubItems[Column].Text);
            done:

            if (Order == SortOrder.Descending)

                result *= -1;
            return result;

        }
    }
}
