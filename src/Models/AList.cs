using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Models
{
    public class AList
    {
        #region Properties
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public List<AListItem> Items { get; set; }
        #endregion

        #region Constuct / Destruct
        public AList()
        {
            Items = new List<AListItem>();
        }
        #endregion

        #region Sub Classes
        [Table("ListItems")]
        public class AListItem
        {
            public int ID { get; set; }

            public int AListID { get; set; }

            public string Description { get; set; }

            public bool Completed { get; set; }
        }
        #endregion
    }
}