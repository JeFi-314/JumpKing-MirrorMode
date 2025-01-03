using HarmonyLib;
using JK = JumpKing.GameManager;
using System.Reflection;
using MirrorMode.Models;
using Microsoft.Xna.Framework;
using JumpKing;
using System.Runtime.CompilerServices;
using System;

namespace MirrorMode.Patching
{
    // Mirror babe art
    public class EndingPictureScreen
    {
        public EndingPictureScreen (Harmony harmony)
        {
            Type type = typeof(JK.EndingPictureScreen);
            MethodInfo Draw = type.GetMethod("Draw");
            harmony.Patch(
                Draw,
                prefix: new HarmonyMethod(AccessTools.Method(typeof(EndingPictureScreen), nameof(preDraw))),
                postfix: new HarmonyMethod(AccessTools.Method(typeof(EndingPictureScreen), nameof(postDraw)))
            );
            
        }

        private static void preDraw() 
        {
            if (MirrorMode.Preferences.IsMirrorBabeArt)
            {
                SpriteBatchManager.StartMirrorBatch();
                SpriteBatchManager.Switch2MirrorBatch();
            }
        }
        private static void postDraw() 
        {
            if (MirrorMode.Preferences.IsMirrorBabeArt)
            {
                SpriteBatchManager.Switch2NormalBatch();
                SpriteBatchManager.Flush();
            }
        }
    }
}