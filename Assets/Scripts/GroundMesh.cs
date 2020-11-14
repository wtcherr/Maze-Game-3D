using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMesh
{
    private Vector3 StartPos;
    private int xSide,zSide,xVert,zVert;
    float xDiff,zDiff;
    private float perlinShift;
    Vector3[] vertices;
    int[] triangles;
    public Vector3[] Vertices{get=>vertices;}
    public int[] Triangles{get=>triangles;}
    public GroundMesh(Vector3 pos,int zSide,int xSide,int xVert,int zVert,float perlinShift){
        this.StartPos=pos;
        this.xSide=xSide;
        this.zSide=zSide;
        this.xDiff = (float) xSide / (xVert-1);
        this.zDiff = (float) zSide / (zVert-1);
        this.xVert=xVert;
        this.zVert=zVert;
        this.perlinShift=perlinShift;
        vertices = new Vector3[xVert * zVert];
        triangles = new int[xVert * zVert * 6];
        CreateMeshVertices();
        CreateMeshTriangles();
    }
    private void CreateMeshVertices(){
        for(int i=0;i<zVert;i++){
            for(int j=0;j<xVert;j++){
                float perlinValue = Mathf.PerlinNoise(i * 0.1f , j * 0.1f) * 10f;
                vertices[i * xVert + j] = new Vector3(StartPos.x + j * xDiff ,StartPos.y + perlinValue ,StartPos.z + i * zDiff);
            }
        }
    }
    private void CreateMeshTriangles(){
        for(int i=0,k=0;i<zVert-1;i++){
            for(int j=0;j<xVert-1;j++,k+=6){
                triangles[k + 0] = j + (xVert * i);
                triangles[k + 1] = j + (xVert * i) + xVert;
                triangles[k + 2] = j + (xVert * i) + 1;
                triangles[k + 3] = j + (xVert * i) + 1;
                triangles[k + 4] = j + (xVert * i) + xVert;
                triangles[k + 5] = j + (xVert * i) + xVert + 1;
            }
        }
    }
}
