using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int length,width,xCellNo,yCellNo,cellSide,cellBorder,Seed;
    private float mxCellDist=0;
    private int[] dx={0,1,0,-1};
    private int[] dy={1,0,-1,0};
    private Cell[,] cells;
    public int Length{get=>length;set=>length=value;}
    public int Width{get=>width;set=>width=value;}
    public Cell[,] Cells{get=>cells;}
    public struct Coordinate{
        public int i,j;
        public Coordinate(int i,int j){
            this.i=i;
            this.j=j;
        }
    }
    Coordinate finish;
    public Coordinate Finish{get=>finish;set=>finish=value;}
    public bool ValidCell(int i,int j){
        return (i>=0 && j>=0 && i<yCellNo && j<xCellNo);
    }
    public Grid(int length,int width,int cellSide,int cellBorder,int Seed){
        this.length=length;
        this.width=width;
        this.cellSide=cellSide;
        this.cellBorder=cellBorder;
        this.xCellNo=width/(cellSide+cellBorder);
        this.yCellNo=length/(cellSide+cellBorder);
        this.Seed=Seed;
        finish=new Coordinate(xCellNo/2,yCellNo/2);
        CreateGrid();
        CreateMaze();
        Unvisit();
        BFS(finish);
        Colorize();
    }
    public void CreateGrid(){
        cells=new Cell[yCellNo,xCellNo];
        for(int i=0;i<yCellNo;i++){
            for(int j=0;j<xCellNo;j++){
                cells[i,j]=new Cell(i,j,cellSide,cellBorder);
            }
        }
    }
    public void CreateMaze(){
        UnityEngine.Random.InitState(Seed);
        Coordinate cur=new Coordinate(0,0);
        Stack stack=new Stack();
        stack.Push(cur);
        while(stack.Count>0){
            cells[cur.i,cur.j].Visited=true;
            var found=false;
            int k=(int)UnityEngine.Random.Range(0, 3);
            for(int i=0;i<dx.Length;i++){
                int index=(i+k)%4;
                var next=new Coordinate(cur.i+dy[index],cur.j+dx[index]);
                if(ValidCell(next.i,next.j)){
                    if(!cells[next.i,next.j].Visited){
                        RemoveWalls(cur,next,index);
                        stack.Push(cur);
                        cur=new Coordinate(next.i,next.j);
                        found=true;
                        break;
                    }
                }
            }
            if(!found && stack.Count>0){
                cur=(Coordinate)stack.Peek();
                stack.Pop();
            }
        }
    }
    public void CheckCoords(){
        for(int i=0;i<cells.GetLength(0);i++){
            string b= "";
            for(int j=0;j<cells.GetLength(1);j++){
                b+=cells[i,j].Dist+" ";
            }
            Debug.Log(b.ToString());
        }
    }
    public void RemoveWalls(Coordinate cur,Coordinate next,int index){
        if(index==0){
            cells[cur.i,cur.j].Walls[0]=false;
            cells[next.i,next.j].Walls[2]=false;
        }else if(index==1){
            cells[cur.i,cur.j].Walls[1]=false;
            cells[next.i,next.j].Walls[3]=false;
        }else if(index==2){
            cells[cur.i,cur.j].Walls[2]=false;
            cells[next.i,next.j].Walls[0]=false;
        }else if(index==3){
            cells[cur.i,cur.j].Walls[3]=false;
            cells[next.i,next.j].Walls[1]=false;
        }
    }
    public void BFS(Coordinate start){
        Queue queue=new Queue();
        queue.Enqueue(start);
        cells[start.i,start.j].Dist=0;
        while(queue.Count>0){
            Coordinate cur=(Coordinate)queue.Peek();
            cells[cur.i,cur.j].Visited=true;
            queue.Dequeue();
            for(int i=0;i<4;i++){
                Coordinate child=new Coordinate(cur.i+dy[i],cur.j+dx[i]);
                if(ValidCell(child.i,child.j)){
                    if(!cells[child.i,child.j].Visited && !cells[cur.i,cur.j].Walls[i]){
                        queue.Enqueue(child);
                        cells[child.i,child.j].Dist=cells[cur.i,cur.j].Dist+1;
                        mxCellDist=Mathf.Max(mxCellDist,cells[cur.i,cur.j].Dist+1);
                    }
                }
            }
        }
    }
    public void Unvisit(){
        for(int i=0;i<yCellNo;i++){
            for(int j=0;j<xCellNo;j++){
                cells[i,j].Visited=false;
            }
        }
    }
    public void Colorize(){
        for(int i=0;i<yCellNo;i++){
            for(int j=0;j<xCellNo;j++){
                byte dist=(byte)((cells[i,j].Dist/mxCellDist)*255);
                cells[i,j].Color=new Color32(dist,0,0,0);
            }
        }
    }
}
