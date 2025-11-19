using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.User
{
	public class UpdateUserRoleRequest
	{
		public int? UserId { get; set; }
		public string Role { get; set; }
	}
}
