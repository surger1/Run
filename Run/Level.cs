using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Run
{
    class Level
    {
        List<Thing> Clouds;
        List<Building> buildings;
        int height;
        long progress;
        long nextSpawn;
        Player player;
        LiveThing Missle;
        LiveThing AttackShip;
        LiveThing PassengerPlane;
        LiveThing FighterJet;
        Thing Doom;
        const int buildingTypes = 3;
        int untilDisaster;
        bool MissleFired;
        bool Ridden;
        bool PlayerDead;
        bool complete;
        public bool seriouslyDone;
        public TimeSpan PlayingLevel;
        public int Bonus;
        public int number;
        bool freewayDone;
        bool freewayLock;
        long freewayDistance;
        long freewayStart;
        List<Person> people;
        Vector2 vel;
        
        
        Thing cityscape;
        const int minHeight = 0;
        const int maxHeight = -500;
        public int goal;
        int soFar = 0;

        public Level(int goalLength,int num)
        {
            vel = new Vector2(((float)Maths.random.NextDouble() - 0.5f), 0);
            people = new List<Person>();
            freewayDistance = Maths.random.Next(7000) + 4000;
            freewayStart = 0;
            number = num;
            seriouslyDone = false;
            Doom = new Thing("DoomWall", new Vector2(-1500 + (ApocalypseParkour.Difficulty * 168), 0), 0);
            Doom.MyAnimationFlyWeight.gotoAnim("Doom",true);
            if (!ApocalypseParkour.leisureMode)
            {
                Doom.Velocity = new Vector2(Player.topSpeed - 4 + (ApocalypseParkour.Difficulty * 1.315f), 0);
            }
            goal = goalLength;
            MissleFired = false;
            height = maxHeight + 100;
            buildings = new List<Building>();
            Clouds = new List<Thing>();
            //Clouds.Add();
            player = new Player("Sean", new Vector2(-10,maxHeight - 100), 0);
            if (ApocalypseParkour.Horde)
            {
                for (int i = 0; i < 100; ++i)
                {
                    AddPerson(new Vector2(0 + Maths.random.Next(500), maxHeight - +Maths.random.Next(50)));
                    people[i].Velocity = new Vector2(1 + Maths.random.Next(7), 1 + Maths.random.Next(7));
                }
            }
            player.Velocity = new Vector2(5, 5);
            
            buildings.Add(new Building(new Vector2(100, height), 0));
            if (!buildings[buildings.Count - 1].invulnerable)
            {
                progress += (int)buildings[buildings.Count - 1].width * 32;
                
            }
            else
            {
                progress += globals._Sprites[buildings[buildings.Count - 1].MyAnimationFlyWeight.SpriteName].Width;
            }

            while (progress < goal * Maths.PixelsInAMeter)
            {
                SpawnNewBuidling(false);
            }
            
            SpawnNewBuidling(true);
            goal = (int)(buildings[buildings.Count - 1].Position.X + 100) / Maths.PixelsInAMeter;
            nextSpawn = 0;
            untilDisaster = Maths.random.Next(10);
            Missle = new LiveThing("Missle",new Vector2(-2000,-2000),0);
            Missle.Velocity = new Vector2(20.0f, 0.0f);
            Ridden = false;
            PlayerDead = false;
            cityscape = new Thing("CityScape", new Vector2(0, maxHeight), 0);
        }

        public void AddPerson(Vector2 pos)
        {
            people.Add(new Person(pos, 0));
        }

        public void Update(GameTime gameTime)
        {
            if(ApocalypseParkour.Horde)
            {
                while(people.Count < 100)
                {
                    try
                    {
                        AddPerson( player.Position);
                        people[people.Count - 1].Velocity = player.Velocity;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            if (AttackShip != null)
            {
                AttackShip.Update(gameTime);
                if(Maths.distance(AttackShip.Position,player.Position) > 3000)
                {
                    AttackShip = null;
                }
            }
            else
            {
                if (Maths.random.Next(700) == 0)
                {

                        AttackShip = new LiveThing("AttackShip", new Vector2(player.Position.X + 2500, player.Position.Y - 600 + Maths.random.Next(1800)), 0);
                        AttackShip.Velocity = new Vector2((22 + Maths.random.Next(7)) * -1, -1 + Maths.random.Next(2));
                        AttackShip.AddEmitter(new Vector2(1220, 260), new Vector2(6, 6), 1, 0, 5000, "JetStream", true);
                        AttackShip.AddEmitter(new Vector2(1220, 270), new Vector2(6, 6), 1, 0, 5000, "JetStream", true);
                        AttackShip.AddEmitter(new Vector2(1220, 280), new Vector2(6, 6), 1, 0, 5000, "JetStream", true);

                }
            }

            if (PassengerPlane != null)
            {
                PassengerPlane.Update(gameTime);
                if (PassengerPlane.Position.Y > 3000 * 1.2)
                {
                    PassengerPlane = null;
                }

                
            }
            else
            {
                if (Maths.random.Next(900) == 0)
                {
                    PassengerPlane = new LiveThing("PassengerPlane", new Vector2(player.Position.X * 0.2f - 400, player.Position.Y * 0.2f - 600), 0);
                    PassengerPlane.Velocity = new Vector2(6,3);
                    PassengerPlane.AddEmitter(new Vector2(24, 18), new Vector2(6,6), 1, 10, 5000, "SmallSmoke", true);
                    PassengerPlane.AddEmitter(new Vector2(24, 22), new Vector2(6, 6), 10, 2, 5000, "Flamming", true);
                }
            }

            if (FighterJet != null)
            {
                FighterJet.Update(gameTime);
                //FighterJet.Position = player.Position;
                if (Maths.distance(FighterJet.Position, player.Position) > 3000)
                {
                    FighterJet = null;
                }
            }
            else
            {
                if (Maths.random.Next(700) == 0)
                {

                    FighterJet = new LiveThing("FighterJet", new Vector2(player.Position.X - 2500, player.Position.Y - 300 + Maths.random.Next(150)), 0);
                    FighterJet.Velocity = new Vector2(25, 0);
                    FighterJet.AddEmitter(new Vector2(-24, 38), new Vector2(6, 6), 1, 0, 5000, "JetStream", true);
                    if (!MissleFired)
                    {
                        MissleFired = true;
                        Missle.Position = FighterJet.Position + new Vector2(0, 100);
                        Ridden = false;
                    }
                }
            }

            if (!complete)
            {
                PlayingLevel += gameTime.ElapsedGameTime;
            }
            foreach (Person p in people)
            {
                p.OnGround = false;
                p.OnEdge = false;
            }
            player.OnGround = false;
            Doom.Update(gameTime);
            for (int cl = 0; cl < Clouds.Count;++cl )
            {
                Clouds[cl].Update(gameTime);
                if(player.Position.X - ((Camera.Position.X * 0.95f) - 700 + Clouds[cl].Position.X) > 2000)
                {
                    Clouds.RemoveAt(cl);
                }
            }

            while (Clouds.Count < 50)
            {
                Clouds.Add(new Thing("Cloud" + Maths.random.Next(2).ToString(), new Vector2(Maths.random.Next(soFar), Maths.random.Next(340)), 0));
                Clouds[Clouds.Count - 1].Velocity = vel;
                soFar += Maths.random.Next(250);
            }

            if (buildings.Count > 0 && buildings[0].Position.X + (buildings[0].width * 32) - player.Position.X < -3000)
            {
                buildings.RemoveAt(0);
            }
            int i = 0;
            foreach (Building build in buildings)
            {
                if (i > 4)
                {
                    break;
                }
                foreach (BoundingBox b in build.boundingBoxes)
                {

                    if (Maths.regionInRegion(new Vector4(player.Position + new Vector2(player.Velocity.X, 0), player.BBox.Y, player.BBox.X), new Vector4(b.position + build.Position, b.dimensions.Y, b.dimensions.X)))
                    {

                        player.Velocity = new Vector2(player.Velocity.X * -0.5f, player.Velocity.Y);

                        if (player.Position.Y - 30 < b.position.Y + build.Position.Y && !player.ledgeGrab)
                        {
                            player.ledgeGrab = true;
                            player.Position = new Vector2(player.Position.X, b.position.Y + build.Position.Y);
                            player.Velocity = new Vector2(0, 0);
                        }


                    }
                    if (Maths.regionInRegion(new Vector4(player.Position + new Vector2(0, player.Velocity.Y), player.BBox.Y, player.BBox.X), new Vector4(b.position + build.Position, b.dimensions.Y, b.dimensions.X)))
                    {
                        if (player.Velocity.Y > 0)
                        {
                            player.OnGround = true;
                            if (build.invulnerable && !build.freeWay)
                            {
                                player.OnMetal = true;
                            }
                            else
                            {
                                player.OnMetal = false;
                            }
                        }
                        if (!player.ledgeGrab)
                        {
                            player.Velocity = new Vector2(player.Velocity.X, Maths.distance(new Vector2(b.position.X + build.Position.X, player.Position.Y + player.BBox.Y), b.position + build.Position));
                        }
                    }

                    if (MissleFired)
                    {
                        if (Maths.regionInRegion(new Vector4(Missle.Position, Missle.BBox.Y, Missle.BBox.X), new Vector4(b.position + build.Position, b.dimensions.Y, b.dimensions.X)))
                        {
                            Camera.shake(500, 20.0f, 30.0f);
                            build.MakeCrumbly(true);
                            MissleFired = false;

                                globals._Sounds["BuildingHit"].Play();
                            
                        }
                    }
                    foreach (Person p in people)
                    {
                        if (Maths.regionInRegion(new Vector4(p.Position + new Vector2(p.Velocity.X + 48 + Maths.random.Next(96), 0), p.BBox.Y, p.BBox.X), new Vector4(b.position + build.Position, b.dimensions.Y, b.dimensions.X)))
                        {
                            if (p.OnGround)
                            {
                                p.OnEdge = true;
                            }
                            else
                            {
                                if (Maths.regionInRegion(new Vector4(p.Position + new Vector2(p.Velocity.X, 0), p.BBox.Y, p.BBox.X), new Vector4(b.position + build.Position, b.dimensions.Y, b.dimensions.X)))
                                {
                                    p.Velocity = new Vector2(p.Velocity.X * -0.2f, p.Velocity.Y);
                                }
                            }
                        }
                        if (Maths.regionInRegion(new Vector4(p.Position + new Vector2(0, p.Velocity.Y), p.BBox.Y, p.BBox.X), new Vector4(b.position + build.Position, b.dimensions.Y, b.dimensions.X)))
                        {
                            if (p.Velocity.Y > 0)
                            {
                                p.checkTerminal();
                                p.OnGround = true;
                                if (build.invulnerable && !build.freeWay)
                                {
                                    p.OnMetal = true;
                                }
                                else
                                {
                                    p.OnMetal = false;
                                }
                                if (p.Position.X > b.position.X + build.Position.X + b.dimensions.X - (32 + Maths.random.Next(32)))
                                {
                                    p.OnEdge = true;
                                    p.minJump = true;
                                }
                                p.Velocity = new Vector2(p.Velocity.X, Maths.distance(new Vector2(b.position.X + build.Position.X, p.Position.Y + p.BBox.Y), b.position + build.Position));
                            }
                        }
                        if (Maths.regionXCompare(new Vector4(p.Position + new Vector2(0, p.Velocity.Y), p.BBox.Y, p.BBox.X), new Vector4(build.Position + new Vector2(-300.0f, 0), build.Height, build.Width + 300.0f)))
                        {
                            if (build.ZombieCheck(new Vector4(p.Position + new Vector2(p.Velocity.X + 12 + Maths.random.Next(24), p.Velocity.Y), p.BBox.Y, p.BBox.X), p.Velocity.X, false,true))
                            {
                                p.OnEdge = true;
                                p.minJump = true;
                            }
                            if (build.ZombieCheck(new Vector4(p.Position + new Vector2(p.Velocity.X, p.Velocity.Y), p.BBox.Y, p.BBox.X), p.Velocity.X,false,false))
                            {
                                p.dead = true;
                                p.Velocity = Vector2.Zero;
                            }
                            if (build.HorseManCheck(new Vector4(p.Position + new Vector2(p.Velocity.X + 64 + Maths.random.Next(64), p.Velocity.Y), p.BBox.Y, p.BBox.X), p.Velocity.X))
                            {
                                p.OnEdge = true;
                                p.minJump = true;
                            }
                            if (build.HorseManCheck(new Vector4(p.Position + new Vector2(p.Velocity.X, p.Velocity.Y), p.BBox.Y, p.BBox.X), p.Velocity.X))
                            {
                                    p.Velocity = new Vector2(-6.0f, -7.0f);
                                    if (Maths.random.Next(3) == 0)
                                    {
                                        p.dead = true;
                                    }
                            }
                            
                        }
                    }
                }
                if (((int)(player.Position.X - 100.0f) / Maths.PixelsInAMeter) >= goal && !complete && (player.OnGround || player.OnMissle))
                {
                    player.LoseControl = true;
                    complete = true;
                    ApocalypseParkour.DoBonus();
                }
                if (Maths.regionXCompare(new Vector4(player.Position + new Vector2(0, player.Velocity.Y), player.BBox.Y, player.BBox.X), new Vector4(build.Position + new Vector2(-800.0f, -720), build.Height, build.Width + 300.0f)))
                {
                    if (Maths.random.Next(2) == 0 && !build.peopleSpawned)
                    {
                        if (!ApocalypseParkour.Horde)
                        {
                            for (int peeps = 0; peeps < Maths.random.Next(3) + 1; peeps++)
                            {
                                try
                                {
                                    AddPerson(build.Position + new Vector2(Maths.random.Next(build.width * 32 - 300) + 300, -95));
                                    people[people.Count - 1].Velocity = new Vector2(6, Maths.random.Next(40));
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                    }
                    build.peopleSpawned = true;
                }

                if (player.Position.X > build.Position.X + (build.width * 32) + 300)
                {
                    if (!ApocalypseParkour.Horde)
                    {
                        if (Maths.random.Next(2) == 0 && !build.peopleSpawnAfter)
                        {
                            try
                            {
                                AddPerson(build.Position + new Vector2(((build.width * 32)) - 300, -51));
                                people[people.Count - 1].Velocity = new Vector2(15, 0);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }

                    build.peopleSpawnAfter = true;
                }

                if (Maths.regionXCompare(new Vector4(player.Position + new Vector2(0, player.Velocity.Y), player.BBox.Y, player.BBox.X), new Vector4(build.Position + new Vector2(-300.0f, 0), build.Height, build.Width + 300.0f)))
                {
                    build.BeginFirinMahLazor();
                    if (build.ZombieCheck(new Vector4(player.Position + new Vector2(player.Velocity.X, player.Velocity.Y), player.BBox.Y, player.BBox.X), player.Velocity.X, true, false))
                    {
                        if (!ApocalypseParkour.GodMode)
                        {
                            player.Velocity = new Vector2(player.Velocity.X * 0.6f, player.Velocity.Y);
                        }
                    }
                    if (build.HorseManCheck(new Vector4(player.Position + new Vector2(player.Velocity.X, player.Velocity.Y), player.BBox.Y, player.BBox.X), player.Velocity.X))
                    {
                        if (!ApocalypseParkour.GodMode)
                        {
                            player.Velocity = new Vector2(-6.0f, -7.0f);
                        }
                    }
                    build.LaunchMeteor();
                    build.RideOn();

                    
                    if (Maths.regionXCompare(new Vector4(player.Position + new Vector2(0, player.Velocity.Y), player.BBox.Y, player.BBox.X), new Vector4(build.Position, build.Height, build.Width)))
                    {
                        build.Disturb();
                        if (!ApocalypseParkour.GodMode)
                        {
                            build.Crumble();
                        }

                    }
                }
                
                i++;
            }
            
            if (MissleFired)
            {
                Missle.Update(gameTime);
                if (Missle.Position.X - player.Position.X > 4000)
                {
                    MissleFired = false;
                }
                if (Maths.regionInRegion(new Vector4(player.Position + new Vector2(player.Velocity.X, 0), player.BBox.Y, player.BBox.X), new Vector4(Missle.Position, Missle.BBox.Y, Missle.BBox.X)))
                {
                    if (!Ridden)
                    {
                        player.OnMissle = true;
                        Ridden = true;
                    }
                    
                }
                if (player.OnMissle)
                {
                    player.Position = new Vector2(Missle.Position.X + 48, Missle.Position.Y - 50.0f);
                    player.Velocity = Missle.Velocity;
                    Missle.Velocity = new Vector2(20.0f, 1.5f);
                }
                else
                {
                    Missle.Velocity = new Vector2(28.0f, 0.0f);
                }
                globals._SoundInstances["JetEngine"].Play();
                float dist = Maths.distance(player.Position,Missle.Position);
                if(dist > 2000.0f)
                {
                    dist = 0.0f;
                }
                else
                {
                    dist = (2000.0f - dist) / 2000.0f; 
                }
                globals._SoundInstances["JetEngine"].Volume = 0.6f * dist;
                if(Missle.Position.X < player.Position.X)
                {
                    globals._SoundInstances["JetEngine"].Pan = (1.0f - dist) * -1.0f;
                }
                else
                {
                    globals._SoundInstances["JetEngine"].Pan = (1.0f - dist);
                }
            }
            else
            {
                player.OnMissle = false;
                globals._SoundInstances["JetEngine"].Stop();

            }
            
            if (player.Position.X < Camera.Position.X + 750)
            {
                player.Update(gameTime);
            }
            for (int p = 0; p < people.Count;++p)
            {
                people[p].Update(gameTime);
                if (people[p].Position.Y > 1500)
                {
                    people.RemoveAt(p); 
                }
            }
            if (Doom.Position.X > Camera.Position.X + 750)
            {
                seriouslyDone = true;
            }
            foreach (Building b in buildings)
            {
                b.Update(gameTime);
            }

            cityscape.Position = new Vector2(Camera.Position.X * 0.9f, (Camera.Position.Y * 0.9f) - 300.0f); 
            
        }

        public bool LevelCompleted()
        {
            return complete;
        }

        public void Draw(GameTime gameTime)
        {

            globals.DrawSpriteStretched("Sky" + number.ToString(),gameTime, new Vector2(Camera.Position.X - 940, Camera.Position.Y - 360),new Vector2(4000,1), Color.White,false);
            
            foreach (Thing t in Clouds)
            {
                t.MyAnimationFlyWeight.Draw(gameTime, new Vector2((Camera.Position.X * 0.95f) - 700 + t.Position.X,(Camera.Position.Y * 0.95f) - 500.0f + t.Position.Y), SpriteEffects.None, 1.0f, Color.White, 1.0f);
            }
            
            for (int op = -1; op < 5; ++op)
            {
                cityscape.Position = new Vector2((Camera.Position.X * 0.9f) + (2700 * op), (Camera.Position.Y * 0.9f) - 300.0f); 
                cityscape.Draw(gameTime);
            }
            
            if (PassengerPlane != null)
            {
                //player.Position = new Vector2((Camera.Position.X * 0.8f) + PassengerPlane.Position.X, (Camera.Position.Y * 0.8f) + PassengerPlane.Position.Y);
                
                foreach (ParticleEmitter p in PassengerPlane.Emitters)
                {
                    p.Draw(gameTime,new Vector2((Camera.Position.X * 0.8f),(Camera.Position.Y * 0.8f)));
                }
                PassengerPlane.MyAnimationFlyWeight.Draw(gameTime, new Vector2((Camera.Position.X * 0.8f) + PassengerPlane.Position.X, (Camera.Position.Y * 0.8f) + PassengerPlane.Position.Y), SpriteEffects.None, 1.0f, Color.White, 1.0f);
            }
            globals._SpriteBatch.DrawString(globals._SpriteFonts["FIPPS"], ((int)(player.Position.X - 100.0f) / Maths.PixelsInAMeter).ToString() +"m", new Vector2(20,20), Color.Black);
            globals._SpriteBatch.DrawString(globals._SpriteFonts["FIPPS"], ((int)(player.Position.X - 100.0f) / Maths.PixelsInAMeter).ToString() + "m", new Vector2(19, 19), Color.White);

            int i = 0;
            if (AttackShip != null)
            {
                AttackShip.Draw(gameTime);
            }
            if (FighterJet != null)
            {
                FighterJet.Draw(gameTime);
            }
            foreach (Building b in buildings)
            {
                if (i > 4)
                {
                    break;
                }
                b.DrawBefore(gameTime);
                i++;
            }

            player.Draw(gameTime);
            foreach (Person p in people)
            {
                p.Draw(gameTime);
            }
            i = 0;

            foreach (Building b in buildings)
            {
                if (i > 4)
                {
                    break;
                }
                b.Draw(gameTime);
                i++;
            }
            if (MissleFired)
            {
                Missle.Draw(gameTime);
            }

            Doom.MyAnimationFlyWeight.Draw(gameTime, new Vector2(Doom.Position.X, Camera.Position.Y - 360), SpriteEffects.None, 1.0f, Color.Black, 1.0f);
            globals.DrawRectangle(gameTime, new Vector2(Doom.Position.X, Camera.Position.Y - 360) - new Vector2(1800, 0), new Vector2(1800, 720), Color.Black, false);
        }

        public bool IsDead()
        {
            if (!complete && (player.Position.Y > 1500 ||  player.Position.X + 50 < Doom.Position.X))
            {
                PlayerDead = true;
                Camera.flash(500, Color.White, true, false);
            }
            return PlayerDead;



        }

        public void SpawnNewBuidling(bool noDisasters)
        {
            nextSpawn = progress;
            int nextBuild = Maths.random.Next(5);
            if (height < maxHeight)
            {
                nextBuild = 3;
            }
            else if(height > minHeight)
            {
                nextBuild = 0;
            }

            if (height >= 0 && !freewayDone && !freewayLock)
            {
                freewayLock = true;
                freewayStart = progress;
            }

            if (freewayLock)
            {
                freewayLock = true;
                progress += 400;
                buildings.Add(new Building(new Vector2(progress, height), 1));
                if (progress - freewayStart > freewayDistance)
                {
                    freewayDone = true;
                    freewayLock = false;
                }
            }
            else
            {

            
                switch(nextBuild)
                {
                    //Building slightly above
                    case 0:
                        height -= 75 + Maths.random.Next(75);
                        progress += 300;
                        buildings.Add(new Building(new Vector2(progress, height), 0));
                        break;
                    //Building pretty over above
                    case 1:
                        height -= 50 + Maths.random.Next(75);
                        progress += 500;
                        buildings.Add(new Building(new Vector2(progress, height), 0));
                        break;
                    //Building slightly lower
                    case 2:
                        height += 100;
                        progress += 500 + Maths.random.Next(100) ;
                        buildings.Add(new Building(new Vector2(progress, height), 0));

                        break;
                    //Building greatly lower
                    case 3:
                        height += 300;
                        progress += 500 + Maths.random.Next(200);
                        buildings.Add(new Building(new Vector2(progress, height), 0));
                        break;
                    //Building same height
                    case 4:
                        progress += 400 + Maths.random.Next(50);
                        buildings.Add(new Building(new Vector2(progress, height), 0));
                        break;

                }
            }
            untilDisaster--;
            if (ApocalypseParkour.MultiHazard)
            {
                if (Maths.random.Next(2) == 0)
                {
                    buildings[buildings.Count - 1].MakeCrumbly(false);
                }
                if (Maths.random.Next(2) == 0)
                {
                    buildings[buildings.Count - 1].Infest();
                }
                if (Maths.random.Next(2) == 0)
                {
                    buildings[buildings.Count - 1].AddMeteor();
                }
                if (Maths.random.Next(2) == 0)
                {
                    buildings[buildings.Count - 1].AddHorseMan();
                }
                if (Maths.random.Next(2) == 0)
                {
                    buildings[buildings.Count - 1].Invade();
                }
            }
            else
            {
                if (untilDisaster <= 0 && !noDisasters)
                {
                    switch (Maths.random.Next(5))
                    {
                        case 0:
                            buildings[buildings.Count - 1].MakeCrumbly(false);
                            break;
                        case 1:
                            buildings[buildings.Count - 1].Infest();
                            break;
                        case 2:
                            buildings[buildings.Count - 1].AddMeteor();
                            break;
                        case 3:
                            buildings[buildings.Count - 1].AddHorseMan();
                            break;
                        case 4:
                            buildings[buildings.Count - 1].Invade();
                            break;
                    }
                    untilDisaster = Maths.random.Next(4 - ApocalypseParkour.Difficulty);
                }
            }
            if (Maths.random.Next(5) == 0 && !freewayLock)
            {
                buildings[buildings.Count - 1].Flock();
            }
            if (!buildings[buildings.Count - 1].invulnerable || buildings[buildings.Count - 1].freeWay)
            {
                progress += (int)buildings[buildings.Count - 1].width * 32;
            }
            else
            {
                progress += globals._Sprites[buildings[buildings.Count - 1].MyAnimationFlyWeight.SpriteName].Width;
            }

        }
    }
}
