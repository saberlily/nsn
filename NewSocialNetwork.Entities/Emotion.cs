﻿

using Castle.ActiveRecord;
namespace NewSocialNetwork.Entities
{
    [ActiveRecord("[NSN.Emotion]", "dbo", Lazy = true)]
    public class Emotion : ActiveRecordBase<Emotion>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "EmotionId")]
        public virtual byte EmotionId { get; set; }

        [BelongsTo("PackagePath", NotNull = true)]
        public virtual EmotionPackage PackagePath { get; set; }

        [Property("Title", Length = 100, NotNull = true)]
        public virtual string Title { get; set; }

        [Property("Text", Length = 20, NotNull = true)]
        public virtual string Text { get; set; }

        [Property("Image", Length = 100, NotNull = true)]
        public virtual string Image { get; set; }

        [Property("Ordering", NotNull = true)]
        public virtual byte Ordering { get; set; }

        public Emotion() { }
    }
}
