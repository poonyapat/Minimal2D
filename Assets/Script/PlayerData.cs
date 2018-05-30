using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

[DataContract]
public class PlayerData : MonoBehaviour
{
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
    private string playerName;
    [DataMember]
    private int latestGameLevel = 0;
    [DataMember]
    private int level = 1;
    [DataMember]
    private float maxExp = 100;
    [DataMember]
    private float curExp = 0;
    [DataMember]
    private int skillPoint = 0;
    [DataMember]
    private int statPoint = 0;

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

    // unstable status
    private int lifePoint = 3;
    private float curHp = 100;
    private float curSta = 100;
    //private bool jumping = true;
    // is attacked
    private const float immortalTime = 0.25f;
    private float latestAttacked = -0.25f;


    public static void LoadStatData()
    {
        maxHpData[0] = 100;
        maxStaData[0] = 100;
        regStaData[0] = 0.1f;
        walkSpeedData[0] = 0.075f;
        runCostData[0] = 0.75f;
        jumpCostData[0] = 30;
        jumpPowerData[0] = 12;
        for (int i = 1; i < 100; i++)
        {
            float reductiveRatio = Mathf.Pow(0.95f, i - 1);
            maxHpData[i] = maxHpData[i - 1] + 0.07f * maxHpData[0] * reductiveRatio;
            maxStaData[i] = maxStaData[i - 1] + 0.05f * maxStaData[0] * reductiveRatio;
            regStaData[i] = regStaData[i - 1] + 0.05f * regStaData[0] * reductiveRatio;
            walkSpeedData[i] = walkSpeedData[i - 1] + 0.03f * walkSpeedData[0] * reductiveRatio;
            runCostData[i] = runCostData[i - 1] - 0.03f * runCostData[0] * reductiveRatio;
            jumpCostData[i] = jumpCostData[i - 1] - 0.03f * jumpCostData[0] * reductiveRatio;
            jumpPowerData[i] = jumpPowerData[i - 1] + 0.03f * jumpPowerData[0] * reductiveRatio;
        }
    }

    public void SetAll(PlayerData pd)
    {
        playerName = pd.playerName;
        latestGameLevel = pd.latestGameLevel;
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

    public void RegenerateStamina()
    {
        CurSta += RegSta;
    }

    public void RegenerateStaminaAtSleep()
    {
        CurSta += RegStaSlp;
    }

    public void DecreaseLifePoint()
    {
        lifePoint--;
    }


    public bool IsImmortal()
    {
        return Time.time < latestAttacked + immortalTime;
    }


    public bool IsJumpable()
    {
        return curSta > JumpCost;
    }

    public bool IsRunnable()
    {
        return curSta > RunCost;
    }

    public void UpdateLatestAttackTime()
    {
        latestAttacked = Time.time;
    }

    public void IncreaseExp(float exp)
    {
        curExp += exp;
        if (curExp >= maxExp)
            LevelUp();
    }

    private void LevelUp()
    {
        level++;
        curExp -= maxExp;
        maxExp *= 1.15f;
        skillPoint++;
        statPoint++;
        if (curExp >= maxExp)
            LevelUp();

    }

    public void IncreaseGameLevel()
    {
        latestGameLevel++;
    }

    // increase stamina regen
    public void IncreaseStaminRegen()
    {
        regStaLevel++;
        statPoint--;
    }
    // increase max hp
    public void IncreaseMaxHp()
    {
        maxHpLevel++;
        statPoint--;
    }
    // increase max stamina
    public void IncreaseMaxSta()
    {
        maxStaLevel++;
        statPoint--;
    }
    // increase base speed
    public void IncreaseWalkSpeed()
    {
        movementSpeedLevel++;
        statPoint--;
    }

    public void DecreaseMovementCost()
    {
        movementCostLevel++;
        statPoint--;
    }
    // increase jump power
    public void IncreaseJumpPower()
    {
        jumpPowerLevel++;
        statPoint--;
    }

    public string PlayerName{
        get {
            return playerName;
        }
        set {
            if (value.Length > 0)
                playerName = value;
        }
    }

    public int LatestGameLevel
    {
        get
        {
            return latestGameLevel;
        }
    }


    public int MaxHpLevel
    {
        get
        {
            return maxHpLevel;
        }
    }

    public int MaxStaLevel
    {
        get
        {
            return maxStaLevel;
        }
    }

    public int RegStaLevel
    {
        get
        {
            return regStaLevel;
        }
    }

    public int MovementSpeedLevel
    {
        get
        {
            return movementSpeedLevel;
        }
    }

    public int MovementCostLevel
    {
        get
        {
            return movementCostLevel;
        }
    }

    public int JumpPowerLevel
    {
        get
        {
            return jumpPowerLevel;
        }
    }

    public int LifePoint
    {
        get
        {
            return lifePoint;
        }
    }

    public float CurHp
    {
        get
        {
            return curHp;
        }
        set
        {
            if (curHp < 0)
                curHp = 0;
            else if (curHp > MaxHp)
                curHp = MaxHp;
            else
                curHp = value;
        }
    }

    public float CurSta
    {
        get
        {
            return curSta;
        }
        set
        {
            if (curSta < 0)
                curSta = 0;
            else if (curSta > MaxSta)
                curSta = MaxSta;
            else
                curSta = value;
        }
    }


    public int Level
    {
        get
        {
            return level;
        }
    }

    public float MaxHp
    {
        get
        {
            return maxHpData[maxHpLevel];
        }
    }

    public float MaxSta
    {
        get
        {
            return maxStaData[maxStaLevel];
        }
    }

    public float RegSta
    {
        get
        {
            return regStaData[regStaLevel];
        }
    }

    public float RegStaSlp
    {
        get
        {
            return regStaSlp * regStaData[regStaLevel];
        }
    }

    public float RunSpeed
    {
        get
        {
            return runSpeed * walkSpeedData[movementSpeedLevel];
        }
    }

    public float WalkSpeed
    {
        get
        {
            return walkSpeedData[movementSpeedLevel];
        }
    }

    public float RunCost
    {
        get
        {
            return runCostData[movementCostLevel];
        }
    }

    public float JumpCost
    {
        get
        {
            return jumpCostData[movementCostLevel];
        }
    }

    public float JumpPower
    {
        get
        {
            return jumpPowerData[jumpPowerLevel];
        }
    }

    public float HpPercentage
    {
        get
        {
            return curHp / MaxHp;
        }
    }

    public float StaPercentage
    {
        get
        {
            return curSta / MaxSta;
        }
    }


    public float ExpPercentage
    {
        get
        {
            return curExp / maxExp;
        }
    }

    public int SkillPoint
    {
        get
        {
            return skillPoint;
        }
    }

    public int StatPoint
    {
        get
        {
            return statPoint;
        }
    }

}