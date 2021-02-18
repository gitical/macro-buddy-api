using System;
using System.ComponentModel.DataAnnotations;

namespace foodTrackerApi.Models
{
    public class FoodEntry
    {
        public virtual long Id { get; set; }

        [Required]
        public virtual Food Food { get; set; }

        public virtual DateTime Timestamp { get; set; }

        [Required]
        public virtual double Amount { get; set; }

        public virtual double TotalCalories
        {
            get { return ((Food.Calories / 100) * Amount); }
        }

        public virtual DateTime? ModifiedDate { get; set; }

        [Required]
        public virtual Guid UserId { get; set; }

    }

}