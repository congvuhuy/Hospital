using Ord.Hospital.AddressType;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Ord.Hospital.Provinces.Dtos
{
    public class ProvinceDto:IEntityDto<int>
    {
        public int Id { get; set; }
        public string ProvinceName { get; set; }
        public int ProvinceCode { get; set; }
        public ProvinceType ProvinceType { get; set; }

        //public DateTime CreationTime { get; set; }
        //public DateTime? LastModificationTime { get; set; }
        //public DateTime? DeletionTime { get; set; }
        //public long? CreatorId { get; set; }
        //public long? LastModifierId { get; set; }
        public long? DeleterId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
