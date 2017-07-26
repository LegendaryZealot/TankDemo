﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankDemo
{
    public partial class MapTest : Form
    {

        //分成40 * 40的格子
        //1080*1920   分为27*48个
        //
        //
        //墙的集合
        List<Wall> wallList = new List<Wall>();
        //Home
        List<Wall> homeList = new List<Wall>();

        public MapTest()
        {

            //
            //
            //做Map测试
            //先不要登录界面
            //
            //Login login = new Login();
            //if (login.ShowDialog() == DialogResult.OK)
            //{
            //    login.Close();
            //}
            InitializeComponent();
        }

        private void MapTest_Load(object sender, EventArgs e)
        {
            ///载入前初始化地图，算是吧
            this.createHome(this.getMapHeight(), this.getMapWidth());
            this.createWall();
            this.drawHome(this.CreateGraphics());
            this.drawWall(this.CreateGraphics());

            ///
            ///
            ///-----------------------------------------------------------------------------------------------///
            
        }

        /// <summary>
        /// 画墙
        /// </summary>
        /// <param name="g"></param>
        public void drawWall(Graphics g)
        {
            foreach (Wall wall in wallList)
            {
                switch (wall.getType())
                {
                    case 0:
                        g.FillRectangle(new SolidBrush(Color.Green), wall.getX() * 40, wall.getY() * 40, Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    case 1:
                        g.FillRectangle(new SolidBrush(Color.Red), wall.getX() * 40, wall.getY() * 40, Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    case 2:
                        g.FillRectangle(new SolidBrush(Color.Yellow), wall.getX() * 40, wall.getY() * 40, Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    case 3:
                        g.FillRectangle(new SolidBrush(Color.Blue), wall.getX() * 40, wall.getY() * 40, Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    default:
                        break;
                }
            }

            

        }
        /// <summary>
        /// 创造墙
        /// </summary>
        public void createWall()
        {
            int mapHeight = getMapHeight();
            int mapWidth = getMapWidth();
            int mapSizeWidth = mapWidth / 40;
            int mapSizeHeight = mapHeight / 40;

            Random ran = new Random();
            while(wallList.Count() != 200)
            {

                int x = ran.Next(mapSizeWidth);
                int y = ran.Next(mapSizeHeight);
                int type = ran.Next(4);
                Wall wall = new Wall();
                wall.setX(x);
                wall.setY(y);
                wall.setType(type);
                if (isInHome(wall) || isInSelf(wall))
           //     if (isInSelf(wall))
                {
                    continue;
                }
                wallList.Add(wall);
                }
        }
        /// <summary>
        /// 创造水晶
        /// </summary>
        /// <param name="mapHeight"></param>
        /// <param name="mapWidth"></param>
        public void createHome(int mapHeight, int mapWidth)
        {
            int mapSizeWidth = mapWidth / 40;
            int mapSizeHeight = mapHeight / 40;
            int startX = mapSizeWidth / 2 - 1;
            int startY = mapSizeHeight - 1;
            for (int i = 0; i < 2; i++)
            {
                int temp = 0;
                for (int j = 0; j < 3; j++)
                {
                    
                    Wall wall = new Wall();
                    wall.setX(startX + temp );
                    wall.setY(startY);
                    temp++;
                    if (i == 1 && j == 1)
                    {
                        wall.setType(5);
                    }
                    else
                    {
                        wall.setType(4);
                    }
                    homeList.Add(wall);
                }
                startY += 1;
            }
        }
        /// <summary>
        /// 画水晶
        /// </summary>
        /// <param name="g"></param>
        public void drawHome(Graphics g)
        {
            foreach (Wall wall in homeList)
            {
                switch (wall.getType())
                {
                        
                    case 4:
                        
                        g.FillRectangle(new SolidBrush(Color.Black), wall.getX() * 40, wall.getY() * 40, Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    case 5:
                        g.FillRectangle(new SolidBrush(Color.Red), wall.getX() * 40, wall.getY() * 40, Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    default:
                        break;

                }
            }
            //在窗口上显示字符串
            Font f = new Font("宋体", 34);
            Brush b;
            b = new SolidBrush(Color.White);
            g.DrawString("家", f, b, homeList[4].getX() * 40 - 10, homeList[4].getY() * 40);
            g.Dispose(); 

        }

       

        /// <summary>
        /// 这里设置为private
        ///因为出错 参数类型权限与函数权限
        ///Wall中某些参数是private的
        ///创建墙的判断
        ///是否重合
        /// </summary>
        /// <param name="wallSelf"></param>
        /// <returns></returns>
        private Boolean isInSelf(Wall wallSelf)
        {
            foreach (Wall wall in wallList)
            {
                if (wallSelf.getX() == wall.getX() && wallSelf.getY() == wall.getY())
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 创建墙的判断
        /// 墙是否和水晶重合
        /// </summary>
        /// <param name="wallSelf"></param>
        /// <returns></returns>
        private Boolean isInHome(Wall wallSelf)
        {
            //foreach (Wall wall in homeList)
            //{
            //    if (wall.getX() == wallSelf.getX() && wall.getY() == wallSelf.getY())
            //    {
            //        return true;
            //    }
                
            //}

            if (wallSelf.getX() >= homeList[0].getX() - 1 && wallSelf.getX() <= homeList[2].getX() + 1 && wallSelf.getY() >= homeList[0].getY() - 2)
            {
                return true;
            }
            return false;
        }
        
        public int getMapHeight()
        {
            return this.Height - Wall.WALL_SIZE;
        }
        public int getMapWidth()
        {
            return this.Width;
        }

        
    }
}