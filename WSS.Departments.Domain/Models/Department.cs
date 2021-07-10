using System.ComponentModel.DataAnnotations;

namespace WSS.Departments.Domain.Models
{
    /// <summary>
    /// Подразделение
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Родительский идентификатор
        /// </summary>
        public int? ParentId { get; set; }
        
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        [MaxLength(90)]
        public string Name { get; set; }
        
        /// <summary>
        /// Версия строки
        /// </summary>
        public byte[] RowVersion { get; set; }
    }
}