using Model.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Sprinkler
    {
        public long Id { get; private set; }
        [StringLength(MaxNameLenght)]
        public string Name { get; private set; }
        public bool IsActive { get; set; }

        private const int MaxNameLenght = 250;

        public Sprinkler(long id)
        {
            Id = id;
        }

        public Result SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure(name, "Name cannot be empty");
            }
            if (name.Length > MaxNameLenght)
            {
                return Result.Failure(name, $"Name cannot be longer than {MaxNameLenght} characters");
            }
            Name = name;
            return Result.Success(name);
        }
    }
}
