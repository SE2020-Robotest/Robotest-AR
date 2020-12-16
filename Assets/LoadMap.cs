using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSharpGRPC.services;
using Msg;


namespace MapOperation
{
    public class LoadMap : MonoBehaviour
    {
        public void loadMap(Map request)
        {
            float roomwidth = (float)request.Roomwidth;
            float roomheight = (float)request.Roomheight;
            
            GameObject exsistingMap = GameObject.Find("mainMap");
            if (exsistingMap == null)
            {
                exsistingMap = new GameObject("mainMap");
                exsistingMap.transform.parent = this.transform;
            }
            else
            {
                Destroy(exsistingMap);
                exsistingMap = new GameObject("mainMap");
                exsistingMap.transform.parent = this.transform;
            }
            /*
            List<GameObject> wallList = new List<GameObject>();
            for (int i = 0; i < 4; i++)
            {
                wallList.Add(GameObject.CreatePrimitive(PrimitiveType.Plane));
                wallList[i].transform.parent = exsistingMap.transform;
            }
            wallList[0].transform.position = new Vector3((float)(roomwidth/20.0),5,0);
            wallList[0].transform.localScale = new Vector3((float)(roomwidth/10.0),1,10);
            wallList[0].transform.localEulerAngles = new Vector3(90,0,0);
            
            wallList[1].transform.position = new Vector3(0,5,(float)(roomheight/20.0));
            wallList[1].transform.localScale = new Vector3((float)(roomheight/10.0),1,10);
            wallList[1].transform.localEulerAngles = new Vector3(0,0,90);
            
            wallList[2].transform.position = new Vector3((float)(roomwidth/20.0),5,(float)(roomheight/10.0));
            wallList[2].transform.localScale = new Vector3((float)(roomwidth/10.0),1,10);
            wallList[2].transform.localEulerAngles = new Vector3(90,0,0);
            
            wallList[3].transform.position = new Vector3((float)(roomwidth/10.0),5,(float)(roomheight/20.0));
            wallList[3].transform.localScale = new Vector3((float)(roomheight/10.0),1,10);
            wallList[3].transform.localEulerAngles = new Vector3(0,0,90);
            */

            GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
            floor.transform.parent = exsistingMap.transform;
            floor.transform.localScale = new Vector3((float)(roomwidth/10.0),5,(float)(roomheight/10.0));
            floor.transform.position = new Vector3((float)(-roomwidth/20.0),0,(float)(-roomheight/20.0));
            
            Google.Protobuf.Collections.RepeatedField<Block> blocks = request.Blocks.Clone();
            foreach (Block block in blocks)
            {
                Block.Types.Type type = block.Type;
                if (type == Block.Types.Type.Cube)
                {
                    GameObject obstacles = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    obstacles.transform.parent = exsistingMap.transform;
					obstacles.transform.localScale = new Vector3((float)(block.W/10.0),2,(float)(block.H/10.0));
					obstacles.transform.position = new Vector3((float)(block.Pos.Posx/10.0),1,(float)(block.Pos.Posy/10.0));
                }
                if (type == Block.Types.Type.Cylinder)
                {
                    GameObject obstacles = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    obstacles.transform.parent = exsistingMap.transform;
					obstacles.transform.localScale = new Vector3((float)(block.W/10.0),1,(float)(block.H/10.0));
					obstacles.transform.position = new Vector3((float)(block.Pos.Posx/10.0),(float)0.5,(float)(block.Pos.Posy/10.0));
                }
            }
        }
    }
}

