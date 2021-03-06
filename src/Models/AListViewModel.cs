using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class AListViewModel
    {
        #region Fields
        private string _Message = null;
        #endregion

        #region Properties
        public int ID { get; set; }

        [Required]
        [Display(Name="List Title:")]
        [MinLength(2, ErrorMessage="You can think of at least 2 characters for the list title!")]
        [MaxLength(50, ErrorMessage="That is a pretty long title don't you think? (less than 50 characters please)")]
        public string Title { get; set; }

        public DateTime LastUpdated { get; set; }

        public List<AListItemViewModel> Items { get; set; }

        public int Total { get; set; }

        public int Completed { get; set; }

        public string Message
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_Message)) { return "<br />"; }
                return _Message;
            }
            set { _Message = value; }
        }
        #endregion

        #region Construct / Destruct
        public AListViewModel()
        {
            Items = new List<AListItemViewModel>();
        }
        #endregion

        #region Conversions
        public static implicit operator AListViewModel(AList from)
        {
            var result = new AListViewModel()
            {
                ID = from.ID,
                Title = from.Title,
                LastUpdated = from.LastUpdated,
                Total = from.Total,
                Completed = from.Completed
            };
            from.Items.ForEach(f => result.Items.Add(f));

            return result;
        }

        public static implicit operator AList(AListViewModel from)
        {
            var result = new AList()
            {
                ID = from.ID,
                Title = from.Title,
                LastUpdated = from.LastUpdated
            };
            from.Items.ForEach(f => result.Items.Add(f));

            return result;
        }
        #endregion

        #region Sub Classes
        public class AListItemViewModel
        {
            #region Properties
            [Required]
            [Display(Name="To Do")]
            [MinLength(2, ErrorMessage="You can think of at least 2 characters for the To Do!")]
            [MaxLength(100, ErrorMessage="That is a pretty long To Do don't you think? (less than 100 characters please)")]
            public string Description { get; set; }

            public bool Completed { get; set; }
            #endregion

            #region Conversions
            public static implicit operator AListItemViewModel(AList.AListItem from)
            {
                return new AListItemViewModel()
                {
                    Description = from.Description,
                    Completed = from.Completed
                };
            }

            public static implicit operator AList.AListItem(AListItemViewModel from)
            {
                return new AList.AListItem()
                {
                    Description = from.Description,
                    Completed = from.Completed
                };
            }
            #endregion
        }
        #endregion
    }
}