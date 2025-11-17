using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
	public class Content : BaseEntity
	{
		public string Title {  get; set; }
		public string Abstract { get; set; }
		public User Author { get; set; }
		public int AuthorId { get; set; }
	}
}
