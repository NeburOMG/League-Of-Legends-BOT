﻿
using System;
using System.Collections.Generic;
using System.Drawing;
using LeagueBot.Patterns;

namespace LeagueBot
{
    public class Coop : PatternScript
    {
        private bool Side
        {
            get;
            set;
        }
        private Point CastTargetPoint
        {
            get;
            set;
        }
        public override void Execute()
        {
            bot.log("waiting for league of legends process...");

            bot.waitProcessOpen(GAME_PROCESS_NAME);

            bot.waitUntilProcessBounds(GAME_PROCESS_NAME, 1030, 797);

            bot.wait(200);

            bot.log("waiting for game to load.");

            bot.bringProcessToFront(GAME_PROCESS_NAME);
            bot.centerProcess(GAME_PROCESS_NAME);

            game.waitUntilGameStart();

            bot.log("We are in game !");

            bot.bringProcessToFront(GAME_PROCESS_NAME);
            bot.centerProcess(GAME_PROCESS_NAME);

            bot.wait(1000);

            Side = game.isBlueSide("SummonersRift");

            if (Side == true)
            {
                CastTargetPoint = new Point(1084, 398);
                bot.log("We are blue side!");
            }
            else
            {
                CastTargetPoint = new Point(644, 761);
                bot.log("We are red side!");
            }


            game.upgradeSpell(1);

            game.talk("Hi guys");

            game.toogleShop();

            game.buyItem(1);
            game.buyItem(2);

            game.toogleShop();

            game.lockAlly(3);

            while (bot.isProcessOpen(GAME_PROCESS_NAME))
            {
                bot.bringProcessToFront(GAME_PROCESS_NAME);

                bot.centerProcess(GAME_PROCESS_NAME);

                game.moveCenterScreen();

                game.castSpell(1, CastTargetPoint.X, CastTargetPoint.Y);


                bot.wait(1000);

                game.moveCenterScreen();

                game.castSpell(2, CastTargetPoint.X, CastTargetPoint.Y);

                bot.wait(1000);

                game.moveCenterScreen();

                game.castSpell(3, CastTargetPoint.X, CastTargetPoint.Y);

                bot.wait(1000);

                game.moveCenterScreen();

                game.castSpell(4, CastTargetPoint.X, CastTargetPoint.Y);

            }


            bot.log("Match ended.");

            bot.waitProcessOpen(CLIENT_PROCESS_NAME);

            bot.bringProcessToFront(CLIENT_PROCESS_NAME);
            bot.centerProcess(CLIENT_PROCESS_NAME);

            bot.wait(5000);

            client.skipHonor();

            bot.wait(2000);

            client.closeGameRecap();

            bot.wait(2000);

            bot.executePattern("StartCoop");
        }
    }
}