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

	private float platformLastPositionX; //yeni levellar ?retmek i?in kullan?lacak

	private enum PlatformType
	{
		None,
		Flat
	}

	private class PlatformPositionInfo
	{
		public PlatformType platType;
		public float positionY;
		public bool hasMonster;
		public bool hasHealthCollectable;

		public PlatformPositionInfo(PlatformType type, float posY, bool has_monster, bool has_collectable)
		{
			platType = type;
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
			platformInfo[currentPlatformInfoIndex].platType = PlatformType.Flat;
			platformInfo[currentPlatformInfoIndex].positionY = 0f;

			currentPlatformInfoIndex++;
		}

		while (currentPlatformInfoIndex < levelLenght - endPlatformLength)
		{
			if (platformInfo[currentPlatformInfoIndex - 1].platType != PlatformType.None)
			{
				currentPlatformInfoIndex++;
				continue;
			}

			float platformPositionY = Random.Range(platformPosition_MinY, platformPosition_MaxY);

			int platformLength = Random.Range(platformLength_Min, platformLength_Max);

			for (int i = 0; i < platformLength; i++)
			{
				bool has_Monster = (Random.Range(0f, 1f) < chanceForMonsterExistence); // chance ini belirlioz
				bool has_healthCollectable = (Random.Range(0f, 1f) < chanceForCollectbaleExistence);

				platformInfo[currentPlatformInfoIndex].platType = PlatformType.Flat;
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
				platformInfo[currentPlatformInfoIndex].platType = PlatformType.Flat;
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

			if (positionInfo.platType == PlatformType.None)
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
					platformPosition = new Vector3(distance_between_platforms + platformLastPositionX, positionInfo.positionY, 0);

				}

				Transform createmonster = (Transform)Instantiate(monster, platformPosition, Quaternion.Euler(0, -90, 0)); //monster ? -90 yapt??mz i?in
				createmonster.parent = monster_parent;
			}

			if (positionInfo.hasHealthCollectable)
			{
                if (gameStarted)
                {
					platformPosition = new Vector3(distance_between_platforms * i, positionInfo.positionY + Random.Range(healthCollectable_MinY, healthCollectable_MaxY), 0);
				}
                else
                {
					platformPosition = new Vector3(distance_between_platforms + platformLastPositionX, positionInfo.positionY + Random.Range(healthCollectable_MinY, healthCollectable_MaxY), 0);
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























































