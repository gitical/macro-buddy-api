using System;

namespace foodTrackerApi.Models
{
    public class Food
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual double Protein { get; set; }
        public virtual double Carbohydrate { get; set; }
        public virtual double Fat { get; set; }
        public virtual double Calories
        {
            get { return (Protein * 4) + (Carbohydrate * 4) + (Fat * 9); }
        }

        public virtual Category Category { get; set; }

        public DateTime? ModifiedDate { get; set; }

    }

}