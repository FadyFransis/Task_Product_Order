using App.Core.Entities.Base;

namespace App.Core.Entities
{
    public class AppSetting : BaseEntity
    {
        public AppSettingKey Key { get; set; }
        public string Value { get; set; }
        public string ValueAr { get; set; }
    }
}
