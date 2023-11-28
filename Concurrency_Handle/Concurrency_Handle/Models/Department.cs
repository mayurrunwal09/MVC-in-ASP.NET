/*namespace Concurrency_Handle.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public byte[] RowVersion { get; set; } // Concurrency token
    }
}
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concurrency_Handle.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }

        [Timestamp] // This attribute specifies a timestamp property for concurrency control.
        public byte[] Timestamp { get; set; }
    }
}
