using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class GridVisualizer : MonoBehaviour
{
    // Start is called before the first frame update
    public int Seed=0;
    [Range(3,1000)]
    public int Length,Width;
    [Range(3,1000)]
    public int zVertices,xVertices;
    [Range(1,100)]
    public int CellSide;
    [Range(0,20)]
    public int CellBorder;
    [Range(1,100)]
    public int wallHeight;
    public GameObject WallPrefab,GroundPrefab,FinishLine;
    private Grid grid;
    private float wallWidth=0.5f;
    public MeshFilter groundMesh;
    public MeshCollider groundCollider;
    private void Start()
    {
        grid=new Grid(Length,Width,CellSide,CellBorder,Seed);
        MakeGroundMesh();
        MakeWalls();
        MakeFinishLine();
    }
    private void MakeGroundMesh(){
        Vector3 startPos=new Vector3(-0.5f * CellSide,0,-0.5f * CellSide);
        GroundMesh mesh=new GroundMesh(startPos,Length,Width,xVertices,zVertices,Seed);
        groundMesh.mesh.vertices=mesh.Vertices;
        groundMesh.mesh.triangles=mesh.Triangles;
        groundMesh.mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh=groundMesh.mesh;
    }
    private void MakeWalls(){
        var Walls=new GameObject();
        Walls.name="Walls";
        var Cells=grid.Cells;
        for(int i=0;i<Cells.GetLength(0);i++){
            for(int j=0;j<Cells.GetLength(1);j++){
                var cell=new GameObject();
                cell.name="Cell "+i+" "+j;
                cell.transform.SetParent(Walls.transform);
                /*if(Cells[i,j].Ground){
                    Vector3 position=new Vector3(Cells[i,j].X,0,Cells[i,j].Y);
                    Quaternion rotation=Quaternion.Euler(90,0,0);
                    var board=Instantiate(WallPrefab,position,rotation,cell.transform);
                    board.transform.localScale=new Vector3(CellSide,CellSide,0.5f);
                    board.GetComponent<Renderer>().material.color=Cells[i,j].Color;
                    board.layer=9;
                    board.name="Ground";
                }*/
                if(Cells[i,j].Walls[0]){
                    Vector3 position=new Vector3(Cells[i,j].X,(wallHeight)/2f,Cells[i,j].Y+(CellSide+CellBorder)/2f);
                    Quaternion rotation=Quaternion.Euler(0,0,0);
                    var wall=Instantiate(WallPrefab,position,rotation,cell.transform);
                    wall.transform.localScale=new Vector3(CellSide,wallHeight,wallWidth);
                    wall.name="NorthWall";
                }
                if(Cells[i,j].Walls[1]){
                    Vector3 position=new Vector3(Cells[i,j].X+(CellSide+CellBorder)/2f,(wallHeight)/2f,Cells[i,j].Y);
                    Quaternion rotation=Quaternion.Euler(0,90,0);
                    var wall=Instantiate(WallPrefab,position,rotation,cell.transform);
                    wall.transform.localScale=new Vector3(CellSide,wallHeight,wallWidth);
                    wall.name="EastWall";
                }
                if(Cells[i,j].Walls[2]){
                    Vector3 position=new Vector3(Cells[i,j].X,(wallHeight)/2f,Cells[i,j].Y-(CellSide+CellBorder)/2f);
                    Quaternion rotation=Quaternion.Euler(0,180,0);
                    var wall=Instantiate(WallPrefab,position,rotation,cell.transform);
                    wall.transform.localScale=new Vector3(CellSide,wallHeight,wallWidth);
                    wall.name="SouthWall";
                }
                if(Cells[i,j].Walls[3]){
                    Vector3 position=new Vector3(Cells[i,j].X-(CellSide+CellBorder)/2f,(wallHeight)/2f,Cells[i,j].Y);
                    Quaternion rotation=Quaternion.Euler(0,270,0);
                    var wall=Instantiate(WallPrefab,position,rotation,cell.transform);
                    wall.transform.localScale=new Vector3(CellSide,wallHeight,wallWidth);
                    wall.name="WestWall";
                }
            }
        }
    }
    private void MakeFinishLine(){
        var position= new Vector3(grid.Cells[grid.Finish.i,grid.Finish.j].X,10,grid.Cells[grid.Finish.i,grid.Finish.j].Y);
        var rotation=Quaternion.Euler(0,0,0);
        var scale=new Vector3(CellSide,wallHeight,CellSide);
        GameObject end=Instantiate(FinishLine,position,rotation);
        end.transform.localScale=scale;
        end.name="end";
    }
}
