using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class MazeGen : MonoBehaviour {
    public class Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
    public enum Movement { goUp = 0, goDown = 1, goLeft = 2, goRight = 3, dontMove = 4 };
    public class MazeCell
    {
        public bool isVisited;
        public bool isInit;
        public bool isFinal;

        /// <summary>
        /// As paredes do maze, se for true então existe uma parede
        /// </summary>
        public bool wallUp,
                    wallDown,
                    wallLeft,
                    wallRight;

        public bool tryUp = false,//false = quer dizer que ainda nao verificou a celula
                    tryDown = false,//true = quer dizer que ja verificou a celula
                    tryLeft = false,
                    tryRight = false;

        public MazeCell()
        {
            isVisited = isInit = false;
            wallUp = wallDown = wallLeft = wallRight = true;
        }
    }

    class Maze
    {
        #region Static
        public static bool isStart = true;
        public static System.Random r = new System.Random();
        public static Point initCellPoint;
        public static Point finishCellPoint;

        public static int numMaxProc = 0;
        #endregion

        public MazeCell[,] maze;

        public Maze(int width, int height)
        {
            maze = new MazeCell[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    maze[i, j] = new MazeCell();
                }
            }
            Movement m = (Movement)r.Next(4);

            switch (m) //Indica onde o labirinto começa, se no lado esq, cima, baixo, ou dir
            {
                case Movement.goUp: //começa em cima
                    ProcessMaze(new Point(r.Next(width), 0), Movement.goUp, 0);
                    break;
                case Movement.goDown: //começa em baixo
                    ProcessMaze(new Point(r.Next(width), height - 1), Movement.goDown, 0);
                    break;
                case Movement.goLeft://começa na esquerda
                    ProcessMaze(new Point(0, r.Next(height)), Movement.goLeft, 0);
                    break;
                case Movement.goRight://começa na direita
                    ProcessMaze(new Point(width - 1, r.Next(height)), Movement.goRight, 0);
                    break;
            }
            //ProcessMaze(new Point(r.Next(width), r.Next(height)), Movement.dontMove);
        }

        public void ProcessMaze(Point currentPoint, Movement direction, int currentNumProc)
        {
            MazeCell currentCell = maze[currentPoint.X, currentPoint.Y]; //usando a currentCell é uma forma mais facil de aceder à celula, em vez de aceder ao array, criando uma linha de codigo muito grande


            if (currentNumProc > Maze.numMaxProc)
            {
                Maze.numMaxProc = currentNumProc;
                Maze.finishCellPoint = currentPoint;
            }

            if (Maze.isStart) //quando o labirinto é chamado pela primeira vez este if é executado
            {
                Maze.initCellPoint = currentPoint; //indica qual é a posicao inicial estaticamente

                currentCell.isInit = true;
                currentCell.isVisited = true;


               /* switch (direction) //parte a parede que server de entrada
                {
                    case Movement.goUp:
                        currentCell.wallUp = false;
                        break;
                    case Movement.goDown:
                        currentCell.wallDown = false;
                        break;
                    case Movement.goLeft:
                        currentCell.wallLeft = false;
                        break;
                    case Movement.goRight:
                        currentCell.wallRight = false;
                        break;
                }*/

                maze[Maze.initCellPoint.X, Maze.initCellPoint.Y] = currentCell;

                Maze.isStart = false;

            }


            //Parte a parede para que haja ligaçao, ligando 2 celulas
            else
            {
                switch (direction)
                {
                    case Movement.goUp:
                        currentCell.wallDown = false;
                        maze[currentPoint.X, currentPoint.Y + 1].wallUp = false;
                        break;
                    case Movement.goDown:
                        currentCell.wallUp = false;
                        maze[currentPoint.X, currentPoint.Y - 1].wallDown = false;
                        break;
                    case Movement.goLeft:
                        currentCell.wallRight = false;
                        maze[currentPoint.X + 1, currentPoint.Y].wallLeft = false;
                        break;
                    case Movement.goRight:
                        currentCell.wallLeft = false;
                        maze[currentPoint.X - 1, currentPoint.Y].wallRight = false;
                        break;

                }
                currentCell.isVisited = true;
                maze[currentPoint.X, currentPoint.Y] = currentCell;
            }//else 


            #region Boundaries check
            //Vê se está perto do limite, pondo o try em true nao deixa que aceda ao elemento out of bounds
            if (currentPoint.X == 0)
            {
                currentCell.tryLeft = true;
            }
            else if (currentPoint.X == maze.GetLength(0) - 1)
            {
                currentCell.tryRight = true;
            }

            if (currentPoint.Y == 0)
            {
                currentCell.tryUp = true;
            }
            else if (currentPoint.Y == maze.GetLength(1) - 1)
            {
                currentCell.tryDown = true;
            }
            #endregion

            maze[currentPoint.X, currentPoint.Y] = currentCell;

            while (true) //cicle que vai correr ate todos os 4 lados da celula tenham sido visitadas, só depois é que volta à funçao recursiva anterior
            {

                #region Verifica Adjacentes
                if (currentPoint.X != 0) //verifica celulas adjacentes
                {
                    if (maze[currentPoint.X - 1, currentPoint.Y].isVisited)
                    {
                        currentCell.tryLeft = true;
                        maze[currentPoint.X - 1, currentPoint.Y].tryRight = true;
                    }
                }

                if (currentPoint.X != maze.GetLength(0) - 1)
                {
                    if (maze[currentPoint.X + 1, currentPoint.Y].isVisited)
                    {
                        currentCell.tryRight = true;
                        maze[currentPoint.X + 1, currentPoint.Y].tryLeft = true;
                    }
                }

                if (currentPoint.Y != 0)
                {
                    if (maze[currentPoint.X, currentPoint.Y - 1].isVisited)
                    {
                        currentCell.tryUp = true;
                        maze[currentPoint.X, currentPoint.Y - 1].tryDown = true;
                    }
                }

                if (currentPoint.Y != maze.GetLength(1) - 1)
                {
                    if (maze[currentPoint.X, currentPoint.Y + 1].isVisited)
                    {
                        currentCell.tryDown = true;
                        maze[currentPoint.X, currentPoint.Y + 1].tryUp = true;
                    }
                }
                #endregion

                maze[currentPoint.X, currentPoint.Y] = currentCell;


                if (currentCell.tryUp && currentCell.tryDown && currentCell.tryLeft && currentCell.tryRight)
                {//Se todas as células à volta foram processadas então a cecula onde está agora é a final
                    currentCell.isFinal = true;

                    maze[currentPoint.X, currentPoint.Y] = currentCell;
                    return;
                }
                else // se não for entao gera um numero e tenta verificar se a celula foi visitada ou nao
                {
                    int dir = r.Next(0, 4);
                    switch (dir)
                    {
                        case 0:
                            if (currentCell.tryUp == false)
                            {
                                currentCell.tryUp = true;
                                maze[currentPoint.X, currentPoint.Y] = currentCell;
                                ProcessMaze(new Point(currentPoint.X, currentPoint.Y - 1), Movement.goUp, currentNumProc + 1);
                            }
                            break;

                        case 1:
                            if (currentCell.tryDown == false)
                            {
                                currentCell.tryDown = true;
                                maze[currentPoint.X, currentPoint.Y] = currentCell;
                                ProcessMaze(new Point(currentPoint.X, currentPoint.Y + 1), Movement.goDown, currentNumProc + 1);
                            }
                            break;

                        case 2:
                            if (currentCell.tryLeft == false)
                            {
                                currentCell.tryLeft = true;
                                maze[currentPoint.X, currentPoint.Y] = currentCell;
                                ProcessMaze(new Point(currentPoint.X - 1, currentPoint.Y), Movement.goLeft, currentNumProc + 1);
                            }
                            break;

                        case 3:
                            if (currentCell.tryRight == false)
                            {
                                currentCell.tryRight = true;
                                maze[currentPoint.X, currentPoint.Y] = currentCell;
                                ProcessMaze(new Point(currentPoint.X + 1, currentPoint.Y), Movement.goRight, currentNumProc + 1);
                            }
                            break;
                    }//switch
                }//else
            }//while
        }//ProcessMaze
    }

    public int width, height;
    public GameObject MazeCellGameObject;
    public GameObject endingObject;

    #region wallPrefabs
    public GameObject[] WallPrefabs;

    enum PrefabEnums:int {Torch = 0, Flag = 1, Painting = 2, Skeleton = 3 };

    #endregion

    private Maze m;
    private CapsuleCollider playerCapsuleCollider;
    // Use this for initialization
    void Start () {
        gameObject.SetActive(true);

        //Reset a variaveis estaticas
        Maze.isStart = true;
        Maze.numMaxProc = 0;

        VerifyHeightWidth();

        playerCapsuleCollider = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<CapsuleCollider>();
        WallPrefabs[1] = GameObject.FindGameObjectsWithTag("Flag")[0]; //capsuleCollider not working properly
        m = new Maze(width, height);
        InstantiateMaze();
        
	}



    private void VerifyHeightWidth()
    {
        if (height <= 1)
            height = 2;

        if (width<= 1)
            width = 2;
    }

    private void InstantiateMaze()
    {
        GameObject MazeParent = new GameObject("Maze");

        int wUp = 1, wDown = 2, wLeft = 4, wRight = 3;
       for (int i = 0; i < MazeCellGameObject.transform.childCount; i++)
        {//indexes the walls so it's faster and easier to search
            if (MazeCellGameObject.transform.GetChild(i).name == "WallDown")
            {
                wDown = i;
            }
            else if(MazeCellGameObject.transform.GetChild(i).name == "WallLeft")
            {
                wLeft = i;
            }
            else if(MazeCellGameObject.transform.GetChild(i).name == "WallRight")
            {
                wRight = i;
            }
            else if(MazeCellGameObject.transform.GetChild(i).name == "WallUp")
            {
                wUp = i;
            }
        }


        for (int i = 0; i <width ; i++)
        {
            for (int j = 0; j < height; j++)
            {
                #region FORCODE
                MazeCell tmpCell = m.maze[i, j];
                GameObject tmpObj = Instantiate(MazeCellGameObject);

                tmpObj.transform.parent = MazeParent.transform; //puts the cell inside the Maze parent so it's nicely organized
                tmpObj.transform.position = new Vector3(10 * (i + 1), 0, (10 * (j + 1))*-1);


                

                if (UnityEngine.Random.value<=.25)
                {
                    GameObject Torch = Instantiate(WallPrefabs[(int)PrefabEnums.Torch]);
                    Torch.transform.position = tmpObj.transform.position;
                    InstantiateWallPrefabs(tmpCell, Torch);
                    Torch.transform.parent = tmpObj.transform;
                }

                if(UnityEngine.Random.value <= .05)
                {
                    GameObject Flag = Instantiate(WallPrefabs[(int)PrefabEnums.Flag]);
                    Flag.transform.position = tmpObj.transform.position;
                    InstantiateWallPrefabs(tmpCell, Flag);
                    Flag.transform.GetChild(2).gameObject.SetActive(true); //Cloth physics
                    Flag.transform.GetChild(2).GetComponent<Cloth>().capsuleColliders[0] = playerCapsuleCollider;
                    Flag.transform.parent = tmpObj.transform;
                }

                if (UnityEngine.Random.value <= .1)
                {
                    GameObject Painting = Instantiate(WallPrefabs[(int)PrefabEnums.Painting]);
                    Painting.transform.position = tmpObj.transform.position;
                    InstantiateWallPrefabs(tmpCell, Painting);
                    Painting.transform.parent = tmpObj.transform;
                }

                if (UnityEngine.Random.value <= .1)
                {
                    GameObject Skeleton = Instantiate(WallPrefabs[(int)PrefabEnums.Skeleton]);
                    Skeleton.transform.position = tmpObj.transform.position;
                    InstantiateWallPrefabs(tmpCell, Skeleton);
                    Skeleton.transform.parent = tmpObj.transform;
                }


                if (tmpCell.wallDown == false)
                {
                    tmpObj.transform.GetChild(wDown).gameObject.SetActive(false);
                }
                else
                {
                    tmpObj.transform.GetChild(wDown).gameObject.SetActive(true);
                }



                if (tmpCell.wallUp == false)
                {
                    tmpObj.transform.GetChild(wUp).gameObject.SetActive(false);
                }
                else
                {
                    tmpObj.transform.GetChild(wUp).gameObject.SetActive(true);
                }




                if (tmpCell.wallLeft == false)
                {
                    tmpObj.transform.GetChild(wLeft).gameObject.SetActive(false);
                }
                else
                {
                    tmpObj.transform.GetChild(wLeft).gameObject.SetActive(true);
                }



                if (tmpCell.wallRight == false)
                {
                    tmpObj.transform.GetChild(wRight).gameObject.SetActive(false);
                }
                else
                {
                    tmpObj.transform.GetChild(wRight).gameObject.SetActive(true);
                }



                tmpObj.name = "cell " + i.ToString() +","+ j.ToString();

                if (tmpCell.isInit)
                {
                    GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
                    player.transform.position = tmpObj.transform.position;
                }
                #endregion
            }
        }

        GameObject instEnding = Instantiate(endingObject);
        instEnding.transform.position = new Vector3(10 * (Maze.finishCellPoint.X + 1), 0, (10 * (Maze.finishCellPoint.Y + 1)) * -1);

    }

    // Update is called once per frame
    void InstantiateWallPrefabs(MazeCell cell, GameObject prefab)
    {
        bool isPlaced = false;
        System.Random r = new System.Random();

        while (isPlaced==false)
        {
            int i = r.Next(0, 4);

            switch (i)
            {
                case 0:
                    if (cell.wallDown) return;
                break;

                case 1:
                    if (cell.wallLeft)
                    {
                        prefab.transform.Rotate(0, 90, 0);
                        isPlaced = true;
                    }
                    break;

                case 2:
                    if (cell.wallUp)
                    {
                        prefab.transform.Rotate(0, 180, 0);
                        isPlaced = true;
                    }
                    break;

                case 3:
                    if (cell.wallRight)
                    {
                        prefab.transform.Rotate(0, 270, 0);
                        isPlaced = true;
                    }
                    break;
            }

        }

    }
}
