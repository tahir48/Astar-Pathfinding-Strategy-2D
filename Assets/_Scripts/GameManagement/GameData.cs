using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
namespace StrategyGame_2DPlatformer.GameManagement
{
    public class GameData : MonoBehaviour
    {
        #region Sprites
        public Sprite populationBuildingSprite;
        public Sprite productionBuildingSprite;
        public Sprite MilitaryBuildingSprite;
        #endregion


        public RectTransform informationMenu;
        public Image buildingsImageUI;

        #region Soldier Prefabs
        public GameObject swordsmanPrefab;
        public GameObject spearmanPrefab;
        public GameObject knightPrefab;
        #endregion

        #region Population Related Variables

        private int _maxPopulationSize;
        public int MaxPopulationSize { get { return _maxPopulationSize; } private set { } }
        #endregion

        #region Simple Sington
        public static GameData instance;
        #endregion

        #region Graph Related Data
        private Graph _graph;
        [SerializeField] private Tilemap _tilemap;
        public Graph Graph { get { return _graph; } private set { } }
        public Tilemap Tilemap { get { return _tilemap; } private set { } }
        #endregion

        #region Population MVP Pattern
        //This class contains population data, therefore it is our Model for MVP pattern
        //This class implements events in population change
        public event Action PopulationChanged; // This event notifies the Presenter that the population has changed.
        private int _currentHumanPopulationSize;
        private int _currentPopulationSize;
        public int CurrentHumanPopulationSize { get { return _currentHumanPopulationSize; } set { _currentHumanPopulationSize = value; } }
        public int CurrentPopulationSize { get { return _currentPopulationSize; } set { _currentPopulationSize = value; } }


        public void IncreaseCurrentHumanPop(int amount)
        {
            _currentHumanPopulationSize += amount;
            UpdatePopulation();
        }

        public void IncreaseCurrentAvailaiblePop(int amount)
        {
            _currentPopulationSize += amount;
            UpdatePopulation();
        }

        public void DecreaseCurrentHumanPop(int amount)
        {
            _currentHumanPopulationSize -= amount;
            UpdatePopulation();
        }

        public void DecreaseCurrentAvailaiblePop(int amount)
        {
            _currentPopulationSize -= amount;
            UpdatePopulation();
        }

        public void UpdatePopulation()
        {
            PopulationChanged.Invoke();
        }

        #endregion

        #region Resources MVP Pattern
        public event Action MoneyChanged;
        private int _money;
        public  int Money { get { return _money; } set { _money = value; } }
        public void IncreaseMoney()
        {
            _money++;
            UpdateMoney();
        }

        public void DecreaseMoney(int amount)
        {
            _money-= amount;
            UpdateMoney();
        }

        public void UpdateMoney()
        {
            MoneyChanged.Invoke();
        }
        #endregion



        private void Awake()
        {
            #region Population Related Variables
            _currentPopulationSize = 5;
            _maxPopulationSize = 200;
            #endregion
            #region Simple Sington
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            #endregion
            #region Graph Related Data
            _graph = new Graph();
            _graph.CreateGraphFromTilemap(_tilemap);
            #endregion
        }
        void Start()
        {
            InitializeHiddenInformationMenu();
        }

        Coroutine cor;
        float lerpDuration = 0.5f;

        public void HideInformationMenu()
        {
            if (cor != null) StopCoroutine(cor);
            cor = StartCoroutine(InformationMenuHider());
        }

        IEnumerator InformationMenuHider()
        {
            float time = 0;
            float startValue = -150;
            float endValue = 150f;
            float menuPosition;
            if (informationMenu == null) yield break;
            Vector2 position = informationMenu.anchoredPosition;

            while (time < lerpDuration)
            {
                menuPosition = Mathf.Lerp(startValue, endValue, time / lerpDuration);
                position.x = menuPosition;
                informationMenu.anchoredPosition = position;
                time += Time.deltaTime;
                yield return null;
            }
            position.x = endValue;
            informationMenu.anchoredPosition = position;
        }

        public void InitializeHiddenInformationMenu()
        {
            Vector2 position = informationMenu.anchoredPosition;
            position.x = 150;
            informationMenu.anchoredPosition = position;
        }

        public void ShowInformationMenu()
        {
            if (cor != null) StopCoroutine(cor);
            cor = StartCoroutine(InformationMenuOpener());
        }

        IEnumerator InformationMenuOpener()
        {
            float time = 0;
            float startValue = 150f;
            float endValue = -150f;
            float menuPosition;
            Vector2 position = informationMenu.anchoredPosition;
            if (informationMenu == null) yield break;
            while (time < lerpDuration)
            {
                menuPosition = Mathf.Lerp(startValue, endValue, time / lerpDuration);
                position.x = menuPosition;
                informationMenu.anchoredPosition = position;
                time += Time.deltaTime;
                yield return null;
            }
            position.x = endValue;
            informationMenu.anchoredPosition = position;
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region Draw Graph OnGizmos
        private void OnDrawGizmos()
        {
            if (_graph != null)
            {
                foreach (Node node in _graph.Nodes)
                {
                    if (node != null)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawSphere(new Vector3(node.x, node.y, 0), 0.1f);

                        foreach (Edge edge in node.edges)
                        {
                            if (edge.endNode != null)
                            {
                                Gizmos.color = Color.green;
                                Gizmos.DrawLine(new Vector3(node.x, node.y, 0), new Vector3(edge.endNode.x, edge.endNode.y, 0));
                            }
                        }
                    }
                }
            }
        }
        #endregion

    }
}
