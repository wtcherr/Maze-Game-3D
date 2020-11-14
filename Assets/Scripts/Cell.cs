using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private int x,y,i,j,dist;
    private bool visited,ground;
    private bool[] walls;
    Color32 color;
    public Color Color{
        get=>color;
        set=>color=value;
    }
    public int X{
        get=>x;
        set=>x=value;
    }
    public int Y{
        get=>y;
        set=>y=value;
    }
    public int I{
        get=>i;
        set=>i=value;
    }
    public int J{
        get=>j;
        set=>j=value;
    }
    public bool Visited{
        get=>visited;
        set=>visited=value;
    }
    public bool Ground{
        get=>ground;
        set=>ground=value;
    }
    public int Dist{
        get=>dist;
        set=>dist=value;
    }
    public bool[] Walls{
        get=>walls;
        set=>walls=value;
    }
    public Cell(int i,int j,int cellSide,int cellBorder){
        this.i=i;
        this.j=j;
        this.x=j*(cellSide+cellBorder);
        this.y=i*(cellSide+cellBorder);
        this.visited=false;
        this.ground=true;
        this.dist=-1;
        this.walls=new bool[]{true,true,true,true};
    }
}
