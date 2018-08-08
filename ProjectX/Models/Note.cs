using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectX.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public List<CheckList> CheckList { get; set; }
        public List<Label> Label { get; set; }
        public bool Pinned { get; set; }
        //public override bool Equals(object Obj)
        //{
        //    Note n = (Note) Obj;
        //    if (this.Title == n.Title && this.Message == n.Message && this.Pinned == n.Pinned && this.CheckList.CheckListIsEquals(n.CheckList) && this.Label.LabelIsEqual(n.Label))
        //        return true;
        //    return false;
        //}

        public bool IsEquals(Note n)
        {
            if (this.Title == n.Title && this.Message == n.Message && this.Pinned == n.Pinned && this.CheckList.CheckListIsEquals(n.CheckList) && this.Label.LabelIsEqual(n.Label))
                return true;
            return false;
        }
    }
    public class CheckList {
        public int Id { get; set; }
        public string Checklist { get; set; }
        public bool IsChecked { get; set; }
    }

    public class Label
    {
        public int Id { get; set; }
        public string label { get; set; }
    }

    public static class ExtensionList {
        public static bool CheckListIsEquals(this List<CheckList> present, List<CheckList> n )
        {
            bool flag = false;
            int count = 0;
            for (int i = 0; i < present.Count; i++)
            {
                if (present[i].Checklist == n[i].Checklist && present[i].IsChecked == n[i].IsChecked)
                    count++;
            }
            if (count == present.Count)
                flag = true;
            return flag;
        }
        public static bool LabelIsEqual(this List<Label> present, List<Label> n)
        {
            bool flag = false;
            int count = 0;
            for (int i = 0; i < present.Count; i++)
            {
                if (present[i].label == n[i].label)
                    count++;
            }
            if (count == present.Count)
                flag = true;
            return flag;
        }
    }
    
}
