using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations;

namespace TaskConstructor.EFModels
{

    public class DarkFieldContext : DbContext
    {
        public DarkFieldContext()
            : base("name=WHJBDb")
        {
            //解决团队开发中，多人迁移数据库造成的修改覆盖问题。
        //    Database.SetInitializer<DarkFieldContext>(null);
            Database.SetInitializer<DarkFieldContext>(new CreateDatabaseIfNotExists<DarkFieldContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //表名不用复数形式
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //移除一对多的级联删除约定，想要级联删除可以在 EntityTypeConfiguration<TEntity>的实现类中进行控制
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //多对多启用级联删除约定，不想级联删除可以在删除前判断关联的数据进行拦截
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //限制长度
            //  modelBuilder.Entity<T_Classes>().Property(c => c.Money).HasPrecision(18, 4);


            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
         //   modelBuilder.
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cadet> Cadets { get; set; }

        public DbSet<ItemBank> ItemBank { get; set; }
        public DbSet<ZoneItemBank> ZoneItemBank { get; set; }
        public DbSet<ZoneResponseSum> ZoneResponseSum { get; set; }

    }

    public class Cadet
    {
        public int CadetId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public byte[] Photo { get; set; }

    }

    public class ItemBank
    {
        [Key]
        public int ItemBankId { get; set; }
        [Display(Name = "题目类型")]
        public string ItemGenre { get; set; }
        [Display(Name = "认知任务")]
        public string TaskKind { get; set; }
        [Display(Name = "题目要求")]
        public string QHead { get; set; }   //图片或者文字
        [Display(Name = "题项")]
        public byte[] QBody { get; set; }
        [Display(Name = "备选项")]
        public string QOption { get; set; }

        [Display(Name = "正确答案")]
        public string QAnswer { get; set; }
        public string QAddition { get; set; }

        public string QPathVideoOrPics { get; set; }

        public ItemBank Clone()
        {
            return new ItemBank()
            {
                ItemBankId = this.ItemBankId,
                ItemGenre = this.ItemGenre,

                QHead = this.QHead,
                QBody = this.QBody,
                QOption = this.QOption,
                QAnswer = this.QAnswer,
                QAddition = this.QAddition,
                QPathVideoOrPics = this.QPathVideoOrPics
            };
        }

    }

    public class ZoneItemBank
    {
        [Key]
        public int ZoneItemBankId { get; set; }
        public string ZoneCode { get; set; }   //对应选区编号
        public string QuesIndexes { get; set; }
    }
    public class ZoneResponseSum
    {
        public int ZoneResponseSumId { get; set; }
        public string ZoneCode { get; set; }
        public long TraineeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int QustionCount { get; set; }
        public string ResponseSumString { get; set; }
        public string Task1RightRate { get; set; }
        public string Task2RightRate { get; set; }
        public string Task3RightRate { get; set; }
        public string Task4RightRate { get; set; }
        public string TotalRightRate { get; set; }

    }





  








}
