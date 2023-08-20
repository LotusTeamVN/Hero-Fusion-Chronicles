
using System.Collections.Generic;

namespace HFC.Hero
{
    /// <summary>
    /// Class để lưu trữ các thông tin của hero mà người chơi sở hữu
    /// </summary>
    public class HeroData
    {
        public string heroName;
        public int level;

        /// <summary>
        /// Chỉ số của hero được lấy từ chỉ số trong config tương ứng nhân với hệ số level hiện tại
        /// </summary>
        public Dictionary<AttributeType, Attribute> attributes = new Dictionary<AttributeType, Attribute>();

        public HeroData()
        {

        }
    }
}


