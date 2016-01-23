using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using SharpDX;

namespace EloBuddyHelper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Game_OnLoad;
        }

        static void Game_OnLoad(EventArgs args)
        {
            Bootstrap.Init(null);
            Chat.Print("EloBuddyHelper Loaded!");

            Obj_AI_Base.OnBuffGain  += OnBuffGain;
            Obj_AI_Base.OnBuffLose += OnBuffLose;
        }

        static void OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs buff)
        {
            if(sender.IsMe)
                Chat.Print("Buff Gained: " + buff.Buff.Name);
        }
        static void OnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs buff)
        {
            if (sender.IsMe)
                Chat.Print("Buff Lost: " + buff.Buff.Name);
        }
    }
}
