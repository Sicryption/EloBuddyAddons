using System;
using System.Collections.Generic;
using System.Reflection;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Spells;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using System.Linq;
using System.IO;

namespace UnsignedEvade
{
    class ExtraSpellOverides
    {
        public static void OnGameLoad()
        {
            Obj_AI_Base.OnBuffGain += OnBuffGain;
            Obj_AI_Base.OnBuffLose += OnBuffLoss;

            //for every player in the game, add them to the list of passive spells.
            foreach (string playerName in EntityManager.Heroes.AllHeroes.GetNames())
                SpellDatabase.championSpellsDrawnOnChampion.Add(new Tuple<string, int>(playerName, 0));

            //for every Lee Sin in the game, add them to the Lee Sin list. This is needed as Lee Sin's Q buff is deleted OnProcessSpell
            //.Target also does not work for Lee Sin's Q Dash
            foreach (AIHeroClient unit in EntityManager.Heroes.AllHeroes.Where(a => a.BaseSkinName == "LeeSin"))
                ParticleDatabase.LeeSinQTargets.Add(new Tuple<AIHeroClient, Obj_AI_Base>(unit, null));

            //Graves Q does not come back to Graves. It goes back to where he casted it.
            foreach (AIHeroClient unit in EntityManager.Heroes.AllHeroes.Where(a => a.BaseSkinName == "Graves"))
                ParticleDatabase.GravesQRewind.Add(new Tuple<AIHeroClient, Vector3>(unit, Vector3.Zero));
        }
        
        private static void OnBuffLoss(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs args)
        {

        }

        private static void OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            //Lee Sin Q Target set.
            if (args.Buff.Name == "BlindMonkQOne")
            {
                Tuple<AIHeroClient, Obj_AI_Base> newLeeSinQTarget = null;
                for (int i = 0; i < ParticleDatabase.LeeSinQTargets.Count; i++)
                    if (ParticleDatabase.LeeSinQTargets[i].Item1 == args.Buff.Caster)
                    {
                        newLeeSinQTarget = new Tuple<AIHeroClient, Obj_AI_Base>((AIHeroClient)args.Buff.Caster, sender);
                        ParticleDatabase.LeeSinQTargets.Remove(ParticleDatabase.LeeSinQTargets[i]);
                        break;
                    }
                if (newLeeSinQTarget != null)
                    ParticleDatabase.LeeSinQTargets.Add(newLeeSinQTarget);
                else
                    Console.WriteLine("Lee Sin Q Target does not exist!");
            }

            // I believe I added to for Lee Sin. Unfortunetly, I'm not quite sure what it entirely does, or if it is needed.
            foreach (SpellInfo info in SpellDatabase.activeSpells.Where(a => a.BuffName != ""))
                if (args.Buff.Name == info.BuffName && args.Buff.Caster.Name == info.caster.Name)
                    info.target = sender;
        }

        public static void OnRefreshSpellList()
        {
            //Make sure that Singed Poisons draw for the appropriate time and don't draw too many circles
            foreach (AIHeroClient singed in EntityManager.Heroes.AllHeroes.Where(a => a.ChampionName == "Singed" && a.HasBuff("PoisonTrail")))
            {
                if (singed.IsEnemy || MenuHandler.DrawMenu.GetCheckboxValue("Draw Friendly Spells/Missiles"))
                {
                    if (singed.Position.IsInRangeFromSingedPoison(75f))
                    {
                        ParticleInfo closestSingedPoison = ParticleDatabase.SingedPoisonTrails.OrderBy(a => a.Position.Distance(singed)).FirstOrDefault();
                        if (closestSingedPoison != null)
                            closestSingedPoison.CreationTime = Game.Time;
                    }
                    else
                        ParticleDatabase.SingedPoisonTrails.Add(new ParticleInfo() { Delay = 3.25f, Radius = 80f, CreationTime = Game.Time, Position = singed.Position });
                }
            }
        }

        public static void OnSpellCreation(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args, SpellInfo info, ref SpellInfo newSpellInstance)
        {
            //overrides
            if (info.SpellName == "AkaliSmokeBomb")
                newSpellInstance.endPosition += new Vector3(0, 30f, 0);
            else if (info.SpellName == "MissFortuneBulletTime")
                newSpellInstance.startingDirection = sender.Direction;
            else if (info.SpellName == "CamilleW")
                newSpellInstance.startingDirection = args.End;
            else if (info.MissileName == "GravesQLineMis")
            {
                Tuple<AIHeroClient, Vector3> newGravesQLine = null;
                for (int i = 0; i < ParticleDatabase.GravesQRewind.Count; i++)
                    if (ParticleDatabase.GravesQRewind[i].Item1 == (AIHeroClient)sender)
                    {
                        newGravesQLine = new Tuple<AIHeroClient, Vector3>((AIHeroClient)sender, newSpellInstance.startPosition);
                        ParticleDatabase.GravesQRewind.Remove(ParticleDatabase.GravesQRewind[i]);
                        break;
                    }
                if (newGravesQLine != null)
                    ParticleDatabase.GravesQRewind.Add(newGravesQLine);
                else
                    Chat.Print("We have a problem");
            }
        }
    }
}
