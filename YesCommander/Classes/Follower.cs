using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace YesCommander.Classes
{
    public class Follower
    {
        public string ID;
        public string Name;
        public string NameEN;
        public string NameCN;
        public string NameTCN;
        public int Quolaty;
        public int Level;
        public int ItemLevel;
        public Races Race;
        public Classes Class;
        public bool IsActive;
        public List<Abilities> AbilityCollection;
        public List<Traits> TraitCollection;
        public string ClassStr;

        public Follower(string ID, string name, int quolaty, int level, int itemLevel, string raceName, Classes classType, string classStr, int isActiveIndex, List<int> abilityIndexes, List<int> traitIndexes, string nameEN = null, string nameCN = null, string nameTCN = null )
        {
            this.ID = ID;
            this.Name = name;
            this.Quolaty = quolaty;
            this.Level = level;
            this.ItemLevel = itemLevel;
            this.Race = GetRaceByName( raceName );
            this.Class = classType;
            this.ClassStr = classStr;
            this.IsActive = isActiveIndex == 1 ? true : false;
            this.NameEN = nameEN;
            this.NameCN = nameCN;
            this.NameTCN = nameTCN;

            this.AbilityCollection = new List<Abilities>();
            if ( abilityIndexes.Count > 1 )
            {
                Abilities a1 = GetAbilityById( abilityIndexes[ 0 ] );
                Abilities a2 = GetAbilityById( abilityIndexes[ 1 ] );
                if ( Convert.ToInt16( a1 ) > Convert.ToInt16( a2 ) )
                {
                    this.AbilityCollection.Add( a2 );
                    this.AbilityCollection.Add( a1 );
                }
                else
                {
                    this.AbilityCollection.Add( a1 );
                    this.AbilityCollection.Add( a2 );
                }
            }
            else if ( abilityIndexes.Count == 1 )
                this.AbilityCollection.Add( GetAbilityById( abilityIndexes[ 0 ] ) );

            this.TraitCollection = new List<Traits>();
            foreach ( int j in traitIndexes )
                this.TraitCollection.Add( GetTratById( j ) );
        }

        public enum Abilities
        {
            DangerZones=0,
            DeadlyMinions,
            GroupDamage,
            MagicDebuff,
            MassiveStrike,
            MinionSwarms,
            PowerfulSpell,
            TimedBattle,
            WildAggression,
            Error
        }
        public static string AbilityToChinese( Abilities ability )
        {
            switch ( ability )
            {
                case Abilities.DangerZones: return "危险区域";
                case Abilities.DeadlyMinions: return "致命爪牙";
                case Abilities.GroupDamage: return "群体伤害";
                case Abilities.MagicDebuff: return "魔法减益";
                case Abilities.MassiveStrike: return "重击";
                case Abilities.MinionSwarms: return "爪牙围攻";
                case Abilities.PowerfulSpell: return "强力法术";
                case Abilities.TimedBattle: return "限时战斗";
                case Abilities.WildAggression: return "野生怪物入侵";
                default: return "Error";
            }
        }
        public static Abilities GetAbilityById( int id )
        {
            switch ( id )
            {
                case ( 1 ):
                    return Abilities.WildAggression;
                case ( 2 ):
                    return Abilities.MassiveStrike;
                case ( 3 ):
                    return Abilities.GroupDamage;
                case ( 4 ):
                    return Abilities.MagicDebuff;
                case ( 6 ):
                    return Abilities.DangerZones;
                case ( 7 ):
                    return Abilities.MinionSwarms;
                case ( 8 ):
                    return Abilities.PowerfulSpell;
                case ( 9 ):
                    return Abilities.DeadlyMinions;
                case ( 10 ):
                    return Abilities.TimedBattle;
                case ( 5 ):
                default: return Abilities.Error;
            }
        }
        public static Abilities GetAbilityFromStr( string abilityStr )
        {
            switch ( abilityStr )
            {
                case ( "Danger Zones" ):
                case ( "危险区域" ):
                case ( "危險區域" ):
                    return Abilities.DangerZones;
                case ( "Deadly Minions" ):
                    return Abilities.DeadlyMinions;
                case ( "Group Damage" ):
                    return Abilities.GroupDamage;
                case ( "Magic Debuff" ):
                    return Abilities.MagicDebuff;
                case ( "Massive Strike" ):
                    return Abilities.MassiveStrike;
                case ( "Minion Swarms" ):
                    return Abilities.MinionSwarms;
                case ( "Powerful Spell" ):
                    return Abilities.PowerfulSpell;
                case ( "Timed Battle" ):
                    return Abilities.TimedBattle;
                case ( "Wild Aggression" ):
                    return Abilities.WildAggression;
                default: return Abilities.Error;
            }
        }
        public static ImageSource GetImageFromAbility( Abilities ability )
        {
            switch ( ability )
            {
                case Abilities.DangerZones: return Follower.GetImageFromPicName( "spell_shaman_earthquake.jpg" );
                case Abilities.DeadlyMinions: return Follower.GetImageFromPicName( "achievement_boss_twinorcbrutes.jpg" );
                case Abilities.GroupDamage: return Follower.GetImageFromPicName( "spell_fire_selfdestruct.jpg" );
                case Abilities.MagicDebuff: return Follower.GetImageFromPicName( "spell_shadow_shadowwordpain.jpg" );
                case Abilities.MassiveStrike: return Follower.GetImageFromPicName( "ability_warrior_savageblow.jpg" );
                case Abilities.MinionSwarms: return Follower.GetImageFromPicName( "spell_deathknight_armyofthedead.jpg" );
                case Abilities.PowerfulSpell: return Follower.GetImageFromPicName( "spell_shadow_shadowbolt.jpg" );
                case Abilities.TimedBattle: return Follower.GetImageFromPicName( "spell_holy_borrowedtime.jpg" );
                case Abilities.WildAggression: return Follower.GetImageFromPicName( "spell_nature_reincarnation.jpg" );
                case Abilities.Error: 
                default: return null;
            }
        }
        public static ImageSource GetImageFromPicName( string picName )
        {
            try
            {
                BitmapImage bi = new BitmapImage( new Uri(
                    "pack://application:,,,/YesCommander;component/Resources/" + picName,
                        UriKind.RelativeOrAbsolute ) );
                return bi;
            }
            catch
            {
                return null;
            }
        }
        public static List<Abilities> GetAbilityFromClass( Classes className )
        {
            List<Abilities> abilities = new List<Abilities>();
            switch ( className )
            {
                case Classes.FrostDeathKnight:
                    abilities.AddRange( new Abilities[] { Abilities.MagicDebuff, Abilities.TimedBattle, Abilities.WildAggression, Abilities.PowerfulSpell, Abilities.MinionSwarms } );
                    break;
                case Classes.BloodDeathKnight:
                case Classes.UnHolyDeathKnight:
                    abilities.AddRange( new Abilities[] { Abilities.MassiveStrike, Abilities.TimedBattle, Abilities.WildAggression, Abilities.PowerfulSpell, Abilities.MinionSwarms } );
                    break;
                    
                case Classes.FeralDruid:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.TimedBattle, Abilities.WildAggression, Abilities.DangerZones, Abilities.MassiveStrike } );
                    break;
                case Classes.GuardianDruid:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.WildAggression, Abilities.DangerZones, Abilities.MassiveStrike } );
                    break;
                case Classes.BalanceDruid:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.DangerZones, Abilities.MinionSwarms } );
                    break;
                case Classes.RestorationDruid:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.MagicDebuff, Abilities.GroupDamage, Abilities.MinionSwarms } );
                    break;

                case Classes.BeastMasterHunter:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.DangerZones, Abilities.WildAggression, Abilities.MinionSwarms } );
                    break;
                case Classes.SurvivalHunter:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.DangerZones, Abilities.MassiveStrike, Abilities.MinionSwarms } );
                    break;
                case Classes.MarksmanshipHunter:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.DangerZones, Abilities.PowerfulSpell, Abilities.MinionSwarms } );
                    break;

                case Classes.ArcaneMage:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.TimedBattle, Abilities.PowerfulSpell, Abilities.DangerZones } );
                    break;
                case Classes.FireMage:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.DangerZones, Abilities.PowerfulSpell, Abilities.MinionSwarms } );
                    break;
                case Classes.FrostMage:
                    abilities.AddRange( new Abilities[] { Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.MassiveStrike, Abilities.PowerfulSpell, Abilities.MinionSwarms } );
                    break;

                case Classes.BrewmasterMonk:
                    abilities.AddRange( new Abilities[] { Abilities.DangerZones, Abilities.DeadlyMinions, Abilities.WildAggression, Abilities.MassiveStrike, Abilities.GroupDamage } );
                    break;
                case Classes.WindwalkerMonk:
                    abilities.AddRange( new Abilities[] { Abilities.DangerZones, Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.WildAggression, Abilities.PowerfulSpell } );
                    break;
                case Classes.MistweaverMonk:
                    abilities.AddRange( new Abilities[] { Abilities.DangerZones, Abilities.TimedBattle, Abilities.PowerfulSpell, Abilities.MagicDebuff, Abilities.GroupDamage } );
                    break;
                    
                case Classes.RetributionPaladin:
                    abilities.AddRange( new Abilities[] { Abilities.DeadlyMinions, Abilities.PowerfulSpell, Abilities.TimedBattle, Abilities.MassiveStrike, Abilities.MinionSwarms } );
                    break;
                case Classes.ProtectionPaladin:
                    abilities.AddRange( new Abilities[] { Abilities.DeadlyMinions, Abilities.PowerfulSpell, Abilities.WildAggression, Abilities.MassiveStrike, Abilities.MagicDebuff } );
                    break;
                case Classes.HolyPaladin:
                    abilities.AddRange( new Abilities[] { Abilities.DeadlyMinions, Abilities.PowerfulSpell, Abilities.TimedBattle, Abilities.GroupDamage, Abilities.MagicDebuff } );
                    break;

                case Classes.DisciplinePriest:
                    abilities.AddRange( new Abilities[] { Abilities.MagicDebuff, Abilities.DangerZones, Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.GroupDamage } );
                    break;
                case Classes.HolyPriest:
                    abilities.AddRange( new Abilities[] { Abilities.MagicDebuff, Abilities.DangerZones, Abilities.TimedBattle, Abilities.GroupDamage, Abilities.MinionSwarms } );
                    break;
                case Classes.ShadowPriest:
                    abilities.AddRange( new Abilities[] { Abilities.MagicDebuff, Abilities.DangerZones, Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.MinionSwarms } );
                    break;

                case Classes.AssassinationRogue:
                    abilities.AddRange( new Abilities[] { Abilities.DeadlyMinions, Abilities.DangerZones, Abilities.PowerfulSpell, Abilities.MassiveStrike, Abilities.TimedBattle } );
                    break;
                case Classes.CombatRogue:
                    abilities.AddRange( new Abilities[] { Abilities.DeadlyMinions, Abilities.DangerZones, Abilities.PowerfulSpell, Abilities.MinionSwarms, Abilities.TimedBattle } );
                    break;
                case Classes.SubtletyRogue:
                    abilities.AddRange( new Abilities[] { Abilities.DeadlyMinions, Abilities.DangerZones, Abilities.PowerfulSpell, Abilities.MassiveStrike, Abilities.MinionSwarms } );
                    break;
                    
                case Classes.ElementalShaman:
                    abilities.AddRange( new Abilities[] { Abilities.GroupDamage, Abilities.MinionSwarms, Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.PowerfulSpell } );
                    break;
                case Classes.EnhancementShaman:
                    abilities.AddRange( new Abilities[] { Abilities.GroupDamage, Abilities.MinionSwarms, Abilities.TimedBattle, Abilities.DangerZones, Abilities.DeadlyMinions } );
                    break;
                case Classes.RestorationShaman:
                    abilities.AddRange( new Abilities[] { Abilities.GroupDamage, Abilities.MinionSwarms, Abilities.PowerfulSpell, Abilities.TimedBattle, Abilities.MagicDebuff } );
                    break;

                case Classes.AfflictionWarlock:
                    abilities.AddRange( new Abilities[] { Abilities.PowerfulSpell, Abilities.TimedBattle, Abilities.DeadlyMinions, Abilities.MinionSwarms, Abilities.MagicDebuff } );
                    break;
                case Classes.DemonologyWarlock:
                    abilities.AddRange( new Abilities[] { Abilities.PowerfulSpell, Abilities.TimedBattle, Abilities.TimedBattle, Abilities.MinionSwarms, Abilities.MassiveStrike } );
                    break;
                case Classes.DestructionWarlock:
                    abilities.AddRange( new Abilities[] { Abilities.PowerfulSpell, Abilities.TimedBattle, Abilities.GroupDamage, Abilities.DeadlyMinions, Abilities.TimedBattle } );
                    break;


                case Classes.ArmsWarrior:
                    abilities.AddRange( new Abilities[] { Abilities.MinionSwarms, Abilities.DangerZones, Abilities.PowerfulSpell, Abilities.TimedBattle, Abilities.WildAggression } );
                    break;
                case Classes.FuryWarrior:
                    abilities.AddRange( new Abilities[] { Abilities.MinionSwarms, Abilities.DangerZones, Abilities.PowerfulSpell, Abilities.TimedBattle, Abilities.MassiveStrike } );
                    break;
                case Classes.ProtectionWarrior:
                    abilities.AddRange( new Abilities[] { Abilities.MinionSwarms, Abilities.DangerZones, Abilities.PowerfulSpell, Abilities.MassiveStrike, Abilities.WildAggression } );
                    break;

                default:
                    break;

            }
            return abilities;
        } 

        public enum Races
        {
            熊猫人=0,

            人类,
            侏儒,
            矮人,
            暗夜精灵,
            德莱尼,
            狼人,

            被遗忘者,
            牛头人,
            兽人,
            巨魔,
            血精灵,
            地精, //12

            埃匹希斯守卫,
            高等鸦人,
            鸦人流亡者,
            木精,
            豺狼人,
            机械,
            独眼魔,
            食人魔,
            锦鱼人,
            刃牙虎人,
            猢狲,

            error
        }
        public static Races GetRaceByName( string name )
        {
            Type race = typeof( Races );
            foreach ( var ra in Enum.GetValues( race ) )
            {
                if ( name == ra.ToString() )
                    return (Races)ra;
            }
            return Races.error;
        }

        public enum Traits
        {
            Unknow =-1,

            FastLearner,
            HighStamina,
            BurstOfPower,
            CombatExperience, //3

            Orcslayer, //4
            Ogreslayer,
            Beastslayer,
            Gronnslayer,
            Furyslayer,
            Primalslayer,
            Voidslayer,
            Talonslayer,
            Demonslayer,  //12

            GnomeLover, //13
            Humanist,
            Dwarvenborn,
            ChildOfTheMoon,
            AllyOfArgus,
            CanineCompanion,
            BrewAficionado,
            ChildOfDraenon,
            DeathFascination,
            Totemist,
            VoodooZealot,
            Elvenkind,
            Economist, //25

            LoneWolf,

            Naturalist, //27
            CaveDweller,
            Wastelander,
            Marshwalker,
            Mountaineer,
            ColdBlooded,
            GuerillaFighter,
            Plainsrunner, //34

            Town,

            Dancer, // 36
            EpicMount,//37

            Bodyguard, //38
            HearthstonePro,//39
            Scavenger,
            ExtraTraining, 
            Mining,//42
            Herbalism,
            Alchemy,
            Blacksmithing,
            Enchanting,
            Engineering,
            Inscription,
            Jewelcrafting,
            Leatherworking,
            Tailoring,
            Skinning,//52
            Evergreen,
            Angler
        }
        public static string TraitToChinese( Traits trait )
        {
            switch ( trait )
            {
                case Traits.Orcslayer: return "兽人克星";
                case Traits.Mountaineer: return "登山家";
                case Traits.ColdBlooded: return "冰冷血脉";
                case Traits.Wastelander: return "废土行者";
                case Traits.FastLearner: return "优等生";
                case Traits.Demonslayer: return "恶魔克星";
                case Traits.Beastslayer: return "野兽克星";
                case Traits.Ogreslayer: return "食人魔克星";
                case Traits.Primalslayer: return "原兽克星";
                case Traits.Gronnslayer: return "戈隆克星";
                case Traits.Furyslayer: return "元素克星";
                case Traits.Voidslayer: return "虚空克星";
                case Traits.Talonslayer: return "利爪克星";
                case Traits.Naturalist: return "自然学家";
                case Traits.CaveDweller: return "穴居人";
                case Traits.GuerillaFighter: return "游击队员";
                case Traits.Town: return "城里人";
                case Traits.Marshwalker: return "沼泽行者";
                case Traits.Plainsrunner: return "平原旅人";

                case Traits.Mining: return "采矿";
                case Traits.Herbalism: return "草药学";
                case Traits.Alchemy: return "炼金术";
                case Traits.Blacksmithing: return "锻造";
                case Traits.Enchanting: return "附魔";
                case Traits.Engineering: return "工程学";
                case Traits.Inscription: return "铭文";
                case Traits.Jewelcrafting: return "珠宝加工";
                case Traits.Leatherworking: return "制皮";
                case Traits.Tailoring: return "裁缝";
                case Traits.Skinning: return "剥皮";

                case Traits.GnomeLover: return "侏儒之友";
                case Traits.Humanist: return "人道主义者";
                case Traits.Dwarvenborn: return "矮人血脉";
                case Traits.ChildOfTheMoon: return "月亮之子";
                case Traits.AllyOfArgus: return "阿古斯之友";
                case Traits.CanineCompanion: return "狼犬伙伴";
                case Traits.BrewAficionado: return "酒鬼";
                case Traits.ChildOfDraenon: return "德拉诺之子";
                case Traits.DeathFascination: return "死亡诱惑";
                case Traits.Totemist: return "图腾崇拜者";
                case Traits.VoodooZealot: return "巫术狂信徒";
                case Traits.Elvenkind: return "精灵一族";
                case Traits.Economist: return "经济学家";

                case Traits.HighStamina: return "持久耐力";
                case Traits.BurstOfPower: return "能量爆发";
                case Traits.LoneWolf: return "独来独往";
                case Traits.Scavenger: return "拾荒者";
                case Traits.ExtraTraining: return "额外训练";
                case Traits.CombatExperience: return "实战经验";
                case Traits.EpicMount: return "史诗坐骑";
                case Traits.Angler: return "垂钓大师";
                case Traits.Evergreen: return "林木长青";
                case Traits.Bodyguard: return "看家护院";
                case Traits.Dancer: return "动感舞王";
                case Traits.HearthstonePro: return "炉石专家";

                default: return "Unknow";
            }
        }

        public static string TraitDescriptionToChinese( Traits trait )
        {
            switch ( trait )
            {
                case Traits.Orcslayer: return "提高战胜兽人的几率。";
                case Traits.Mountaineer: return "提高在山地中赢得战斗的几率。";
                case Traits.ColdBlooded: return "提高在雪地中赢得战斗的几率。";
                case Traits.Wastelander: return "提高在沙漠中赢得战斗的几率。";
                case Traits.FastLearner: return "此追随者完成任务时获得的经验提高50%";
                case Traits.Demonslayer: return "提高战胜恶魔的几率。";
                case Traits.Beastslayer: return "提高战胜野兽的几率。";
                case Traits.Ogreslayer: return "提高战胜食人魔的几率。";
                case Traits.Primalslayer: return "提高战胜原兽及其爪牙的几率。";
                case Traits.Gronnslayer: return "提高战胜毁灭者及其爪牙的几率。";
                case Traits.Furyslayer: return "提高战胜元素之怒的几率。";
                case Traits.Voidslayer: return "提高战胜畸变怪和亡灵的几率。";
                case Traits.Talonslayer: return "提高战胜鸦人及其爪牙的几率。";
                case Traits.Naturalist: return "提高在森林中赢得战斗的几率。";
                case Traits.CaveDweller: return "提高在地下赢得战斗的几率。";
                case Traits.GuerillaFighter: return "提高在丛林中赢得战斗的几率。";
                case Traits.Town: return "提高在城镇中赢得战斗的几率。";
                case Traits.Marshwalker: return "提高在沼泽中赢得战斗的几率。";
                case Traits.Plainsrunner: return "提高在平原上赢得战斗的几率。";

                case Traits.Mining: return "被指派到矿井时可解锁矿车，还能提高产品订单的产量。";
                case Traits.Herbalism: return "被指派到药圃时可提供草药学加成，还能提高产品订单的产量。";
                case Traits.Alchemy: return "被指派到炼金实验室时可提供炼金加成。";
                case Traits.Blacksmithing: return "被指派到熔炉时可降低你的物品耐久损失，还能提高产品订单的产量。";
                case Traits.Enchanting: return "被指派到附魔研究室时可提供各种附魔加成。";
                case Traits.Engineering: return "被指派到工坊时可出售工程学装置，还能提高产品订单的产量。";
                case Traits.Inscription: return "被指派到铭文师之家时可以制造特殊的文件，还能提高产品订单的产量。";
                case Traits.Jewelcrafting: return "被指派到珠宝店时可提供各种珠宝加工加成。";
                case Traits.Leatherworking: return "被指派到制革厂时可制造各种帐篷，还能提高产品订单的产量。";
                case Traits.Tailoring: return "被指派到裁缝店时可提供各种裁缝加成。";
                case Traits.Skinning: return "被指派到畜棚时能根据追随者的等级提高产品订单的产量。";

                case Traits.GnomeLover: return "提高与侏儒一起执行任务的成功率。";
                case Traits.Humanist: return "提高与人类一起执行任务的成功率。";
                case Traits.Dwarvenborn: return "提高与矮人一起执行任务的成功率。";
                case Traits.ChildOfTheMoon: return "提高与暗夜精灵一起执行任务的成功率。";
                case Traits.AllyOfArgus: return "提高与德莱尼一起执行任务的成功率。";
                case Traits.CanineCompanion: return "提高与狼人一起执行任务的成功率。";
                case Traits.BrewAficionado: return "提高与熊猫人一起执行任务的成功率。";
                case Traits.ChildOfDraenon: return "提高与兽人一起执行任务的成功率。";
                case Traits.DeathFascination: return "提高与亡灵一起执行任务的成功率。";
                case Traits.Totemist: return "提高与牛头人一起执行任务的成功率。";
                case Traits.VoodooZealot: return "提高与巨魔一起执行任务的成功率。";
                case Traits.Elvenkind: return "提高与血精灵一起执行任务的成功率。";
                case Traits.Economist: return "提高与地精一起执行任务的成功率。";

                case Traits.HighStamina: return "提高持续时间超过7小时的任务的成功率。";
                case Traits.BurstOfPower: return "提高持续时间少于7小时的任务的成功率。";
                case Traits.LoneWolf: return "提高独自执行任务的成功率。";
                case Traits.Scavenger: return "使完成任务后获得的要塞物资提高200%";
                case Traits.ExtraTraining: return "使所有追随者完成任务所获得的经验值提高35%";
                case Traits.CombatExperience: return "提高任务的成功率。";
                case Traits.EpicMount: return "任务时间缩短50%";
                case Traits.Angler: return "有了纳特的知识，你在德拉诺的钓鱼技术使你能够不用鱼饵就钓到大鱼。";
                case Traits.Evergreen: return "被指派到伐木场时，可使你的伐木场更加高效环保地生产木材。";
                case Traits.Bodyguard: return "指派到2级或3级兵营后可作为保镖陪伴你游历德拉诺。";
                case Traits.Dancer: return "你轻盈的步伐能让你在危险区域的移动速度提升一些。";
                case Traits.HearthstonePro: return "一位备受关注的战术大师小队成员的经验获取速度提高35%";

                default: return "Unknow";
            }
        }

        public static string EnvirementToChinese( Traits trait )
        {
            switch ( trait )
            {
                case Traits.Orcslayer: return "兽人";
                case Traits.Mountaineer: return "山地";
                case Traits.ColdBlooded: return "雪地";
                case Traits.Wastelander: return "沙漠";
                case Traits.Demonslayer: return "恶魔";
                case Traits.Beastslayer: return "野兽";
                case Traits.Ogreslayer: return "食人魔";
                case Traits.Primalslayer: return "原兽";
                case Traits.Gronnslayer: return "毁灭者";
                case Traits.Furyslayer: return "元素之怒";
                case Traits.Voidslayer: return "畸变怪";
                case Traits.Talonslayer: return "鸦人";
                case Traits.Naturalist: return "森林";
                case Traits.CaveDweller: return "地穴";
                case Traits.GuerillaFighter: return "丛林";
                case Traits.Town: return "城镇";
                case Traits.Marshwalker: return "沼泽";
                case Traits.Plainsrunner: return "平原";
                default: return "Unknow";
            }
        }
        public static Traits GetTratById( int id )
        {
            switch ( id )
            {
                case 4: return Traits.Orcslayer;
                case 7: return Traits.Mountaineer;
                case 8: return Traits.ColdBlooded;
                case 9: return Traits.Wastelander;
                case 29: return Traits.FastLearner;
                case 36: return Traits.Demonslayer;
                case 37: return Traits.Beastslayer;
                case 38: return Traits.Ogreslayer;
                case 39: return Traits.Primalslayer;
                case 40: return Traits.Gronnslayer;
                case 41: return Traits.Furyslayer;
                case 42: return Traits.Voidslayer;
                case 43: return Traits.Talonslayer;
                case 44: return Traits.Naturalist;
                case 45: return Traits.CaveDweller;
                case 46: return Traits.GuerillaFighter;
                case 47: return Traits.Town;
                case 48: return Traits.Marshwalker;
                case 49: return Traits.Plainsrunner;

                case 52: return Traits.Mining;
                case 53: return Traits.Herbalism;
                case 54: return Traits.Alchemy;
                case 55: return Traits.Blacksmithing;
                case 56: return Traits.Enchanting;
                case 57: return Traits.Engineering;
                case 58: return Traits.Inscription;
                case 59: return Traits.Jewelcrafting;
                case 60: return Traits.Leatherworking;
                case 61: return Traits.Tailoring;
                case 62: return Traits.Skinning;

                case 63: return Traits.GnomeLover;
                case 64: return Traits.Humanist;
                case 65: return Traits.Dwarvenborn;
                case 66: return Traits.ChildOfTheMoon;
                case 67: return Traits.AllyOfArgus;
                case 68: return Traits.CanineCompanion;
                case 69: return Traits.BrewAficionado;
                case 70: return Traits.ChildOfDraenon;
                case 71: return Traits.DeathFascination;
                case 72: return Traits.Totemist;
                case 73: return Traits.VoodooZealot;
                case 74: return Traits.Elvenkind;
                case 75: return Traits.Economist;

                case 76: return Traits.HighStamina;
                case 77: return Traits.BurstOfPower;
                case 78: return Traits.LoneWolf;
                case 79: return Traits.Scavenger;
                case 80: return Traits.ExtraTraining;
                case 201: return Traits.CombatExperience;
                case 221: return Traits.EpicMount;
                case 227: return Traits.Angler;
                case 228: return Traits.Evergreen;
                case 231: return Traits.Bodyguard;
                case 232: return Traits.Dancer;
                case 236: return Traits.HearthstonePro;

                default: return Traits.Unknow;
            }
        }
        public static Traits GetTratByString( string trait )
        {
            switch ( trait )
            {
                case "Orcslayer": return Traits.Orcslayer;
                case "Mountaineer": return Traits.Mountaineer;
                case "ColdBlooded": return Traits.ColdBlooded;
                case "Wastelander": return Traits.Wastelander;
                case "FastLearner": return Traits.FastLearner;
                case "Demonslayer": return Traits.Demonslayer;
                case "Beastslayer": return Traits.Beastslayer;
                case "Ogreslayer": return Traits.Ogreslayer;
                case "Primalslayer": return Traits.Primalslayer;
                case "Gronnslayer": return Traits.Gronnslayer;
                case "Furyslayer": return Traits.Furyslayer;
                case "Voidslayer;": return Traits.Voidslayer;
                case "Talonslayer": return Traits.Talonslayer;
                case "Naturalist": return Traits.Naturalist;
                case "CaveDweller": return Traits.CaveDweller;
                case "GuerillaFighter": return Traits.GuerillaFighter;
                case "Town": return Traits.Town;
                case "Marshwalker": return Traits.Marshwalker;
                case "Plainsrunner": return Traits.Plainsrunner;

                case "Mining": return Traits.Mining;
                case "Herbalism": return Traits.Herbalism;
                case "Alchemy": return Traits.Alchemy;
                case "Blacksmithing": return Traits.Blacksmithing;
                case "Enchanting": return Traits.Enchanting;
                case "Engineering": return Traits.Engineering;
                case "Inscription": return Traits.Inscription;
                case "Jewelcrafting": return Traits.Jewelcrafting;
                case "Leatherworking": return Traits.Leatherworking;
                case "Tailoring": return Traits.Tailoring;
                case "Skinning": return Traits.Skinning;

                case "GnomeLover": return Traits.GnomeLover;
                case "Humanist": return Traits.Humanist;
                case "Dwarvenborn": return Traits.Dwarvenborn;
                case "ChildOfTheMoon": return Traits.ChildOfTheMoon;
                case "AllyOfArgus": return Traits.AllyOfArgus;
                case "CanineCompanion": return Traits.CanineCompanion;
                case "BrewAficionado": return Traits.BrewAficionado;
                case "ChildOfDraenon": return Traits.ChildOfDraenon;
                case "DeathFascination": return Traits.DeathFascination;
                case "Totemist": return Traits.Totemist;
                case "VoodooZealot": return Traits.VoodooZealot;
                case "Elvenkind": return Traits.Elvenkind;
                case "Economist": return Traits.Economist;

                case "HighStamina": return Traits.HighStamina;
                case "BurstOfPower": return Traits.BurstOfPower;
                case "LoneWolf": return Traits.LoneWolf;
                case "Scavenger": return Traits.Scavenger;
                case "ExtraTraining": return Traits.ExtraTraining;
                case "CombatExperience": return Traits.CombatExperience;
                case "EpicMount": return Traits.EpicMount;
                case "Angler": return Traits.Angler;
                case "Evergreen": return Traits.Evergreen;
                case "Bodyguard": return Traits.Bodyguard;
                case "Dancer": return Traits.Dancer;
                case "HearthstonePro": return Traits.HearthstonePro;

                default: return Traits.Unknow;
            }
        }
        public static List<Traits> FilteredRaceTrait( Follower follower )
        {
            List<Traits> result = new List<Traits>();
            foreach ( Traits trait in follower.TraitCollection )
            {
                if ( Follower.IsRaceLoverTrait( trait ) )
                    result.Add( trait );
            }
            return result;
        }
        public static bool IsRaceLoverTrait( Traits trait )
        {
            if ( trait == Traits.Humanist ||
                    trait == Traits.GnomeLover ||
                    trait == Traits.Humanist ||
                    trait == Traits.Dwarvenborn ||
                    trait == Traits.ChildOfTheMoon ||
                    trait == Traits.AllyOfArgus ||
                    trait == Traits.CanineCompanion ||
                    trait == Traits.BrewAficionado ||
                    trait == Traits.ChildOfDraenon ||
                    trait == Traits.DeathFascination ||
                    trait == Traits.Totemist ||
                    trait == Traits.VoodooZealot ||
                    trait == Traits.Elvenkind ||
                    trait == Traits.Economist )
                return true;
            else return false;
        }
        public static Races GetRaceMatchedByTrait( Traits trait )
        {
            switch ( trait )
            {
                case Traits.GnomeLover: return Races.侏儒;
                case Traits.Humanist: return Races.人类;
                case Traits.Dwarvenborn: return Races.矮人;
                case Traits.ChildOfTheMoon: return Races.暗夜精灵;
                case Traits.AllyOfArgus: return Races.德莱尼;
                case Traits.CanineCompanion: return Races.狼人;

                case Traits.BrewAficionado: return Races.熊猫人;

                case Traits.ChildOfDraenon: return Races.兽人;
                case Traits.DeathFascination: return Races.被遗忘者;
                case Traits.Totemist: return Races.牛头人;
                case Traits.VoodooZealot: return Races.巨魔;
                case Traits.Elvenkind: return Races.血精灵;
                case Traits.Economist: return Races.地精;
                default: return Races.error;
            }
        }

        public static Traits GetTraitOfLoverByRace( Races races )
        {
            switch ( races )
            {
                case Races.侏儒: return Traits.GnomeLover;
                case Races.人类: return Traits.Humanist;
                case Races.矮人: return Traits.Dwarvenborn;
                case Races.暗夜精灵: return Traits.ChildOfTheMoon;
                case Races.德莱尼: return Traits.AllyOfArgus;
                case Races.狼人: return Traits.CanineCompanion;

                case Races.熊猫人: return Traits.BrewAficionado;

                case Races.兽人: return Traits.ChildOfDraenon;
                case Races.被遗忘者: return Traits.DeathFascination;
                case Races.牛头人: return Traits.Totemist;
                case Races.巨魔: return Traits.VoodooZealot;
                case Races.血精灵: return Traits.Elvenkind;
                case Races.地精: return Traits.Economist;
                default: return Traits.Unknow;
            }
        }

        public static ImageSource GetImageFromFromTrait( Traits trait )
        {
            if ( trait == Traits.Unknow )
                return null;
            BitmapImage bi = new BitmapImage( new Uri(
                "pack://application:,,,/YesCommander;component/Resources/" + trait.ToString()+".jpg",
                    UriKind.RelativeOrAbsolute ) );
            return bi;
        }

        public enum Classes
        {
            Unknown=-1,

            BloodDeathKnight=0,
            FrostDeathKnight,
            UnHolyDeathKnight,
            BalanceDruid,
            FeralDruid,
            GuardianDruid,
            RestorationDruid,
            BeastMasterHunter,
            MarksmanshipHunter,
            SurvivalHunter,//9
            ArcaneMage,
            FireMage,
            FrostMage,
            BrewmasterMonk,
            MistweaverMonk,
            WindwalkerMonk,
            HolyPaladin,
            ProtectionPaladin,
            RetributionPaladin,
            DisciplinePriest,//19
            HolyPriest,
            ShadowPriest,
            AssassinationRogue,
            CombatRogue,
            SubtletyRogue,
            ElementalShaman,
            EnhancementShaman,
            RestorationShaman,
            AfflictionWarlock,
            DemonologyWarlock,//29
            DestructionWarlock,
            ArmsWarrior,
            FuryWarrior,
            ProtectionWarrior//33
        }
        public static Classes GetClassBySpec( int spec )
        {
            switch ( spec )
            {
                case 2: return Classes.BloodDeathKnight;
                case 3: return Classes.FrostDeathKnight;
                case 4: return  Classes.UnHolyDeathKnight;
                case 5: return Classes.BalanceDruid;

                case 7: return Classes.FeralDruid;
                case 8: return Classes.GuardianDruid;
                case 9: return Classes.RestorationDruid;
                case 10: return Classes.BeastMasterHunter;

                case 12: return Classes.MarksmanshipHunter;
                case 13: return Classes.SurvivalHunter;
                case 14: return Classes.ArcaneMage;
                case 15: return Classes.FireMage;
                case 16: return Classes.FrostMage;
                case 17: return Classes.BrewmasterMonk;
                case 18: return Classes.MistweaverMonk;
                case 19: return Classes.WindwalkerMonk;
                case 20: return Classes.HolyPaladin;
                case 21: return Classes.ProtectionPaladin;
                case 22: return Classes.RetributionPaladin;
                case 23: return Classes.DisciplinePriest;
                case 24: return Classes.HolyPriest;
                case 25: return Classes.ShadowPriest;
                case 26: return Classes.AssassinationRogue;
                case 27: return Classes.CombatRogue;
                case 28: return Classes.SubtletyRogue;
                case 29: return Classes.ElementalShaman;
                case 30: return Classes.EnhancementShaman;
                case 31: return Classes.RestorationShaman;
                case 32: return Classes.AfflictionWarlock;
                case 33: return Classes.DemonologyWarlock;
                case 34: return Classes.DestructionWarlock;
                case 35: return Classes.ArmsWarrior;

                case 37: return Classes.FuryWarrior;
                case 38: return Classes.ProtectionWarrior;
                default: return Classes.Unknown;
            }
        }
        public static Classes GetClassByStr( string classStr, string specStr )
        {
            switch ( classStr )
            {
                case "死亡骑士":
                    switch ( specStr )
                    {
                        case "鲜血": return Classes.BloodDeathKnight;
                        case "冰霜": return Classes.FrostDeathKnight;
                        case "邪恶":
                        default: return Classes.UnHolyDeathKnight;
                    }
                case "德鲁伊":
                    switch ( specStr )
                    {
                        case "平衡": return Classes.BalanceDruid;
                        case "野性": return Classes.FeralDruid;
                        case "恢复": return Classes.RestorationDruid;
                        case "守护":
                        default: return Classes.GuardianDruid;
                    }
                case "战士":
                    switch ( specStr )
                    {
                        case "武器": return Classes.ArmsWarrior;
                        case "狂暴": return Classes.FuryWarrior;
                        case "防护":
                        default: return Classes.ProtectionWarrior;
                    }
                case "术士":
                    switch ( specStr )
                    {
                        case "恶魔": return Classes.DemonologyWarlock;
                        case "痛苦": return Classes.AfflictionWarlock;
                        case "毁灭":
                        default: return Classes.DestructionWarlock;
                    }
                case "武僧":
                    switch ( specStr )
                    {
                        case "织雾": return Classes.MistweaverMonk;
                        case "酿酒": return Classes.BrewmasterMonk;
                        case "踏风":
                        default: return Classes.WindwalkerMonk;
                    }
                case "法师":
                    switch ( specStr )
                    {
                        case "奥术": return Classes.ArcaneMage;
                        case "火焰": return Classes.FireMage;
                        case "冰霜":
                        default: return Classes.FrostMage;
                    }
                case "牧师":
                    switch ( specStr )
                    {
                        case "神圣": return Classes.HolyPriest;
                        case "戒律": return Classes.DisciplinePriest;
                        case "暗影":
                        default: return Classes.ShadowPriest;
                    }
                case "猎人":
                    switch ( specStr )
                    {
                        case "兽王": return Classes.BeastMasterHunter;
                        case "生存": return Classes.SurvivalHunter;
                        case "射击":
                        default: return Classes.MarksmanshipHunter;
                    }
                case "盗贼":
                case "潜行者":
                    switch ( specStr )
                    {
                        case "刺杀": return Classes.AssassinationRogue;
                        case "战斗": return Classes.CombatRogue;
                        case "敏锐":
                        default: return Classes.SubtletyRogue;
                    }
                case "萨满":
                    switch ( specStr )
                    {
                        case "元素": return Classes.ElementalShaman;
                        case "增强": return Classes.EnhancementShaman;
                        case "恢复":
                        default: return Classes.RestorationShaman;
                    }
                case "骑士":
                    switch ( specStr )
                    {
                        case "惩戒": return Classes.RetributionPaladin;
                        case "防护": return Classes.ProtectionPaladin;
                        case "神圣":
                        default: return Classes.HolyPaladin;
                    }
                default: return Classes.Unknown;
            }
        }
        public static Classes GetClassBySingleStr( string str )
        {
            switch ( str )
            {
                case "鲜血-死亡骑士": return Classes.BloodDeathKnight;
                case "冰霜-死亡骑士": return Classes.FrostDeathKnight;
                case "邪恶-死亡骑士": return Classes.UnHolyDeathKnight;
                case "平衡-德鲁伊": return Classes.BalanceDruid;
                case "野性-德鲁伊": return Classes.FeralDruid;
                case "恢复-德鲁伊": return Classes.RestorationDruid;
                case "守护-德鲁伊": return Classes.GuardianDruid;
                case "武器-战士": return Classes.ArmsWarrior;
                case "狂暴-战士": return Classes.FuryWarrior;
                case "防护-战士": return Classes.ProtectionWarrior;
                case "恶魔-术士": return Classes.DemonologyWarlock;
                case "痛苦-术士": return Classes.AfflictionWarlock;
                case "毁灭-术士": return Classes.DestructionWarlock;
                case "织雾-武僧": return Classes.MistweaverMonk;
                case "酿酒-武僧": return Classes.BrewmasterMonk;
                case "踏风-武僧": return Classes.WindwalkerMonk;
                case "奥术-法师": return Classes.ArcaneMage;
                case "火焰-法师": return Classes.FireMage;
                case "冰霜-法师": return Classes.FrostMage;
                case "神圣-牧师": return Classes.HolyPriest;
                case "戒律-牧师": return Classes.DisciplinePriest;
                case "暗影-牧师": return Classes.ShadowPriest;
                case "兽王-猎人": return Classes.BeastMasterHunter;
                case "生存-猎人": return Classes.SurvivalHunter;
                case "射击-猎人": return Classes.MarksmanshipHunter;
                case "刺杀-潜行者":
                case "刺杀-盗贼": return Classes.AssassinationRogue;
                case "战斗-潜行者":
                case "战斗-盗贼": return Classes.CombatRogue;
                case "敏锐-潜行者":
                case "敏锐-盗贼": return Classes.SubtletyRogue;
                case "元素-萨满": return Classes.ElementalShaman;
                case "增强-萨满": return Classes.EnhancementShaman;
                case "恢复-萨满": return Classes.RestorationShaman;
                case "惩戒-圣骑士": return Classes.RetributionPaladin;
                case "防护-圣骑士": return Classes.ProtectionPaladin;
                case "神圣-圣骑士": return Classes.HolyPaladin;
                default: return Classes.Unknown;
            }
        }

        public static string GetCNStringByClass( Classes c )
        {
            switch ( c )
            {
                case Classes.BloodDeathKnight: return "鲜血-死亡骑士";
                case Classes.FrostDeathKnight: return "冰霜-死亡骑士";
                case Classes.UnHolyDeathKnight: return "邪恶-死亡骑士";
                case Classes.BalanceDruid: return "平衡-德鲁伊";
                case Classes.FeralDruid: return "野性-德鲁伊";
                case Classes.RestorationDruid: return "恢复-德鲁伊";
                case Classes.GuardianDruid: return "守护-德鲁伊";
                case Classes.ArmsWarrior: return "武器-战士";
                case Classes.FuryWarrior: return "狂暴-战士";
                case Classes.ProtectionWarrior: return "防护-战士";
                case Classes.DemonologyWarlock: return "恶魔-术士";
                case Classes.AfflictionWarlock: return "痛苦-术士";
                case Classes.DestructionWarlock: return "毁灭-术士";
                case Classes.MistweaverMonk: return "织雾-武僧";
                case Classes.BrewmasterMonk: return "酿酒-武僧";
                case Classes.WindwalkerMonk: return "踏风-武僧";
                case Classes.ArcaneMage: return "奥术-法师";
                case Classes.FireMage: return "火焰-法师";
                case Classes.FrostMage: return "冰霜-法师";
                case Classes.HolyPriest: return "神圣-牧师";
                case Classes.DisciplinePriest: return "戒律-牧师";
                case Classes.ShadowPriest: return "暗影-牧师";
                case Classes.BeastMasterHunter: return "兽王-猎人";
                case Classes.SurvivalHunter: return "生存-猎人";
                case Classes.MarksmanshipHunter: return "射击-猎人";
                case Classes.AssassinationRogue: return "刺杀-潜行者";
                case Classes.CombatRogue: return "战斗-潜行者";
                case Classes.SubtletyRogue: return "敏锐-潜行者";
                case Classes.ElementalShaman: return "元素-萨满";
                case Classes.EnhancementShaman: return "增强-萨满";
                case Classes.RestorationShaman: return "恢复-萨满";
                case Classes.RetributionPaladin: return "惩戒-圣骑士";
                case Classes.ProtectionPaladin: return "防护-圣骑士";
                case Classes.HolyPaladin: return "神圣-圣骑士";
                default: return Classes.Unknown.ToString();
            }
        }
    }
}
