using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

[DataContract]
public class PlayerData : MonoBehaviour {
	private static float[] maxHpData = new float[100];
	private static float[] maxStaData = new float[100];
	private static float[] regStaData = new float[100];
	private static float[] walkSpeedData = new float[100];
	private static float[] runCostData = new float[100];
	private static float[] jumpCostData = new float[100];
	private static float[] jumpPowerData = new float[100];
	private const float regStaSlp = 3;
	private const float runSpeed = 1.75f;

	[DataMember]
	private int level = 1;
	[DataMember]
	private float maxExp = 100;
	[DataMember]
	private float curExp = 0;
	[DataMember]
	public int skillPoint = 0;
	[DataMember]
	public int statPoint = 0;

	[DataMember]
	private int maxHpLevel = 0;
	[DataMember]
	private int maxStaLevel = 0;
	[DataMember]
	private int regStaLevel = 0;
	[DataMember]
	private int movementSpeedLevel = 0;
	[DataMember]
	private int movementCostLevel = 0;
	[DataMember]
	private int jumpPowerLevel = 0;

	PlayerData(){
		LoadStatData ();
	}

	public static void LoadStatData(){
		maxHpData [0] = 100;
		maxStaData [0] = 100;
		regStaData [0] = 0.1f;
		walkSpeedData [0] = 0.075f;
		runCostData [0] = 0.75f;
		jumpCostData [0] = 30;
		jumpPowerData [0] = 12;
		for (int i = 1; i < 100; i++) {
			float reductiveRatio = Mathf.Pow(0.95f, i-1);
			maxHpData [i] = maxHpData [i - 1] + 0.07f * maxHpData [0] * reductiveRatio;
			maxStaData [i] = maxStaData [i - 1] + 0.05f * maxStaData [0] * reductiveRatio;
			regStaData [i] = regStaData [i - 1] + 0.05f * regStaData [0] * reductiveRatio;
			walkSpeedData [i] = walkSpeedData [i - 1] + 0.03f * walkSpeedData [0] * reductiveRatio;
			runCostData [i] = runCostData [i - 1] - 0.03f * runCostData [0] * reductiveRatio;
			jumpCostData [i] = jumpCostData [i - 1] - 0.03f * jumpCostData [0] * reductiveRatio;
			jumpPowerData [i] = jumpPowerData [i - 1] + 0.03f * jumpPowerData [0] * reductiveRatio;
		}
	}

	public void IncreaseExp(float exp){
		curExp += exp;
		if (curExp >= maxExp)
			LevelUp ();
	}

	public int GetLevel(){
		return level;
	}

	public float GetMaxHp(){
		return maxHpData[maxHpLevel];
	}

	public float GetMaxSta(){
		return maxStaData[maxStaLevel];
	}

	public float GetRegSta(){
		return regStaData[regStaLevel];
	}

	public float GetRegStaSlp(){
		return regStaSlp * regStaData[regStaLevel];
	}

	public float GetRunSpeed(){
		return runSpeed * walkSpeedData[movementSpeedLevel];
	}

	public float GetWalkSpeed(){
		return walkSpeedData[movementSpeedLevel];
	}

	public float GetRunCost(){
		return runCostData[movementCostLevel];
	}

	public float GetJumpCost(){
		return jumpCostData[movementCostLevel];
	}

	public float GetJumpPower(){
		return jumpPowerData[jumpPowerLevel];
	}

	private void LevelUp(){
		level++;
		curExp -= maxExp;
		maxExp *= 1.15f;
		skillPoint++;
		statPoint++;
		if (curExp >= maxExp)
			LevelUp ();

	}

	// increase stamina regen
	public void IncreaseStaminRegen(){
		regStaLevel++;
		statPoint--;
	}
	// increase max hp
	public void IncreaseMaxHp(){
		maxHpLevel++;
		statPoint--;
	}
	// increase max stamina
	public void IncreaseMaxSta(){
		maxStaLevel++;
		statPoint--;
	}
	// increase base speed
	public void IncreaseWalkSpeed(){
		movementSpeedLevel++;
		statPoint--;
	}

	public void DecreaseMovementCost(){
		movementCostLevel++;
		statPoint--;
	}
	// increase jump power
	public void IncreaseJumpPower(){
		jumpPowerLevel++;
		statPoint--;
	}

	public float GetExpPrecentage(){
		return curExp / maxExp;
	}

	public void SetAll(PlayerData pd){
		level = pd.level;
		maxExp = pd.maxExp;
		curExp = pd.curExp;
		skillPoint = pd.skillPoint;
		statPoint = pd.statPoint;

		maxHpLevel = pd.maxHpLevel;
		maxStaLevel = pd.maxStaLevel;
		regStaLevel = pd.regStaLevel;
		movementSpeedLevel = pd.movementSpeedLevel;
		movementCostLevel = pd.movementCostLevel;
		jumpPowerLevel = pd.jumpPowerLevel;
	}
}