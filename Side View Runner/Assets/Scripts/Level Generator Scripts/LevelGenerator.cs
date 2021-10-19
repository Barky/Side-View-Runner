//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class LevelGenerator : MonoBehaviour
//{

//    [SerializeField] private int levelLength = 5;
//    [SerializeField] private int startPlatformLength = 5;
//    [SerializeField] private int endPlatformLength = 5;
//    [SerializeField] private int distanceBetweenPlatforms;
//    [SerializeField] private Transform platformPrefab;
//    [SerializeField] private Transform platformParent;
//    [SerializeField] private Transform monster;
//    [SerializeField] private Transform monsterParent;
//    [SerializeField] private Transform healthCollectible;
//    [SerializeField] private Transform healthCollectibleParent;
//    [SerializeField] private float platformPosition_minY = 0f, platformPosition_maxY = 10f;
//    [SerializeField] private int platformLength_min = 1, platformLength_max = 4;
//    [SerializeField] private float chanceForMonsterExistence = 0.25f, chanceForCollectibleExistence = 0.1f;
//    [SerializeField] private float healthCollectible_minY = 1f, healthCollectible_maxY = 3f;
//    [SerializeField] private float platformLastPositionX;


//    private enum PlatformType
//    {
//        None,
//        Flat
//    }


//    private class PlatformPositionInfo
//    {
//        public PlatformType platformType;
//        public float positionY;
//        public bool hasMonster;
//        public bool hasHealthCollectible;

//        public PlatformPositionInfo(PlatformType type, float posy, bool monster, bool health)
//        {
//            platformType = type;
//            positionY = posy;
//            hasMonster = monster;
//            hasHealthCollectible = health;
//        }
//    }
//    private void Start()
//    {
//        GenerateLevel();
//    }
//    void FillOutPositionInfo(PlatformPositionInfo[] platformInfo)
//    {
//        int currentPlatformInfoIndex = 0;
//        Debug.Log(currentPlatformInfoIndex);
//        // sadece ilk platformu oluþturuyoruz
//        for (int i = 0; i < startPlatformLength; i++)   
//        {
//            Debug.Log(currentPlatformInfoIndex);
//            platformInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
//            platformInfo[currentPlatformInfoIndex].positionY = 0f;


//            currentPlatformInfoIndex++;
//        }

//        while(currentPlatformInfoIndex < levelLength - endPlatformLength)
//        {
//            Debug.Log(currentPlatformInfoIndex);
//            if (platformInfo[currentPlatformInfoIndex -1].platformType != PlatformType.None)
//            {

//                currentPlatformInfoIndex++;
//                continue; // while döngüsünün öncesine atar

//            }

//            float platformPositionY = Random.Range(platformPosition_minY, platformPosition_maxY);
//            int platformLength = Random.Range(platformLength_min, platformLength_max);

//            for (int i = 0; i < platformLength; i++)
//            {
//                bool has_Monster = (Random.Range(0f, 1f) < chanceForMonsterExistence); // ihtimale göre true döndürecek
//                bool has_Collectible = (Random.Range(0f,1f) < chanceForCollectibleExistence);

//                platformInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
//                platformInfo[currentPlatformInfoIndex].positionY = platformPositionY;
//                platformInfo[currentPlatformInfoIndex].hasMonster = has_Monster;
//                platformInfo[currentPlatformInfoIndex].hasHealthCollectible = has_Collectible;
//                currentPlatformInfoIndex++;

//                if(currentPlatformInfoIndex > levelLength - endPlatformLength)
//                {
//                    currentPlatformInfoIndex = levelLength - endPlatformLength;
//                    break;
//                }

//            }

//            for ( int i =0; i < endPlatformLength; i++)
//            {
//                platformInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
//                platformInfo[currentPlatformInfoIndex].positionY = 0f;
//                currentPlatformInfoIndex++;
//            }
//        }

//    }

//    void CreatePlatformsFromPositionInfo(PlatformPositionInfo[] platformpositioninfo)
//    {
//        for (int i =0; i < platformpositioninfo.Length; i++)
//        {
//            PlatformPositionInfo positionInfo = platformpositioninfo[i];

//            if(positionInfo.platformType == PlatformType.None)
//            {
//                continue;
//            }
//            //oyun baþladý mý kontrol ettir **to-do
//            Vector3 platformPosition = new Vector3(distanceBetweenPlatforms * i, positionInfo.positionY, 0);
//            //sonralýkla kulanmak için x pozisyonunu kaydet.**to-do

//            Transform createBlock = (Transform) Instantiate(platformPrefab, platformPosition, Quaternion.identity); // oluþturulacak obje, oluþturulacak pozisyon, rotasyon 
//            createBlock.parent = platformParent;

//            if (positionInfo.hasMonster)
//            {
//                //yazýlacak
//            }

//            if (positionInfo.hasHealthCollectible)
//            {
//                //yazýlacak
//            }
//        }
//    }

//    public void GenerateLevel()
//    {
//        PlatformPositionInfo[] platforminfo = new PlatformPositionInfo[levelLength];
//        for(int i=1; i < platforminfo.Length; i++)
//        {
//            platforminfo[i] = new PlatformPositionInfo(PlatformType.None, -1f, false, false);

//        }
//        FillOutPositionInfo(platforminfo);
//        CreatePlatformsFromPositionInfo(platforminfo);
//    }


//}//end
//////
////////
///////
/////


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

	[SerializeField]
	private int levelLenght;

	[SerializeField]
	private int startPlatformLength = 5, endPlatformLength = 5;

	[SerializeField]
	private int distance_between_platforms;

	[SerializeField]
	private Transform platformPrefab, platform_parent;

	[SerializeField]
	private Transform monster, monster_parent;

	[SerializeField]
	private Transform health_Collectable, healthCollectable_parent;

	[SerializeField]
	private float platformPosition_MinY = 0f, platformPosition_MaxY = 10f;

	[SerializeField]
	private int platformLength_Min = 1, platformLength_Max = 4;

	[SerializeField]
	private float chanceForMonsterExistence = 0.25f, chanceForCollectbaleExistence = 0.1f;

	[SerializeField]
	private float healthCollectable_MinY = 1f, healthCollectable_MaxY = 3f;

	private float platformLastPositionX;

	private enum PlatformType
	{
		None,
		Flat
	}

	private class PlatformPositionInfo
	{
		public PlatformType platfromType;
		public float positionY;
		public bool hasMonster;
		public bool hasHealthCollectable;

		public PlatformPositionInfo(PlatformType type, float posY, bool has_monster, bool has_collectable)
		{
			platfromType = type;
			positionY = posY;
			hasMonster = has_monster;
			hasHealthCollectable = has_collectable;
		}

	} // class PlatformPositionInfo

	void Start()
	{
		GenerateLevel(true);
	}

	void FillOutPositionInfo(PlatformPositionInfo[] platformInfo)
	{
		int currentPlatformInfoIndex = 0;

		for (int i = 0; i < startPlatformLength; i++)
		{
			platformInfo[currentPlatformInfoIndex].platfromType = PlatformType.Flat;
			platformInfo[currentPlatformInfoIndex].positionY = 0f;

			currentPlatformInfoIndex++;
		}

		while (currentPlatformInfoIndex < levelLenght - endPlatformLength)
		{
			if (platformInfo[currentPlatformInfoIndex - 1].platfromType != PlatformType.None)
			{
				currentPlatformInfoIndex++;
				continue;
			}

			float platformPositionY = Random.Range(platformPosition_MinY, platformPosition_MaxY);

			int platformLength = Random.Range(platformLength_Min, platformLength_Max);

			for (int i = 0; i < platformLength; i++)
			{
				bool has_Monster = (Random.Range(0f, 1f) < chanceForMonsterExistence);
				bool has_healthCollectable = (Random.Range(0f, 1f) < chanceForCollectbaleExistence);

				platformInfo[currentPlatformInfoIndex].platfromType = PlatformType.Flat;
				platformInfo[currentPlatformInfoIndex].positionY = platformPositionY;
				platformInfo[currentPlatformInfoIndex].hasMonster = has_Monster;
				platformInfo[currentPlatformInfoIndex].hasHealthCollectable = has_healthCollectable;

				currentPlatformInfoIndex++;

				if (currentPlatformInfoIndex > (levelLenght - endPlatformLength))
				{
					currentPlatformInfoIndex = levelLenght - endPlatformLength;
					break;
				}

			}

			for (int i = 0; i < endPlatformLength; i++)
			{
				platformInfo[currentPlatformInfoIndex].platfromType = PlatformType.Flat;
				platformInfo[currentPlatformInfoIndex].positionY = 0f;

				currentPlatformInfoIndex++;
			}

		} // while loop

	}

	void CreatePlatformsFromPositionInfo(PlatformPositionInfo[] platformPositionInfo, bool gameStarted)
	{
		for (int i = 0; i < platformPositionInfo.Length; i++)
		{
			PlatformPositionInfo positionInfo = platformPositionInfo[i];

			if (positionInfo.platfromType == PlatformType.None)
			{
				continue;
			}

			Vector3 platformPosition;

			// here we are going to check if the game is started or not
			if (gameStarted)
			{
				platformPosition = new Vector3(distance_between_platforms * i, positionInfo.positionY, 0);
			}
			else
			{
				platformPosition = new Vector3(distance_between_platforms + platformLastPositionX, positionInfo.positionY, 0);
			}

			// save the platform position x for later use
			platformLastPositionX = platformPosition.x;

			Transform createBlock = (Transform)Instantiate(platformPrefab, platformPosition, Quaternion.identity);
			createBlock.parent = platform_parent;

			if (positionInfo.hasMonster)
			{

				if (gameStarted)
				{
					platformPosition = new Vector3(distance_between_platforms * i, positionInfo.positionY + 0.1f, 0);
				}
				else
				{
					platformPosition = new Vector3(distance_between_platforms + platformLastPositionX, positionInfo.positionY + 0.1f, 0);
				}

				Transform createMonster = (Transform)Instantiate(monster, platformPosition, Quaternion.Euler(0, -90, 0));
				createMonster.parent = monster_parent;

			}

			if (positionInfo.hasHealthCollectable)
			{
				if (gameStarted)
				{
					platformPosition = new Vector3(distance_between_platforms * i,
						positionInfo.positionY + Random.Range(healthCollectable_MinY, healthCollectable_MaxY), 0);
				}
				else
				{
					platformPosition = new Vector3(distance_between_platforms + platformLastPositionX,
						positionInfo.positionY + Random.Range(healthCollectable_MinY, healthCollectable_MaxY), 0);
				}

				Transform createHealthCollectable = (Transform)Instantiate(health_Collectable, platformPosition, Quaternion.identity);
				createHealthCollectable.parent = healthCollectable_parent;
			}

		} // for loop
	}

	public void GenerateLevel(bool gameStarted)
	{
		PlatformPositionInfo[] platformInfo = new PlatformPositionInfo[levelLenght];
		for (int i = 0; i < platformInfo.Length; i++)
		{
			platformInfo[i] = new PlatformPositionInfo(PlatformType.None, -1f, false, false);
		}

		FillOutPositionInfo(platformInfo);
		CreatePlatformsFromPositionInfo(platformInfo, gameStarted);

	}



} // class























































