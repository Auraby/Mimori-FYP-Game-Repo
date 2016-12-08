using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Skill_Ability_Tree : MonoBehaviour {

 /*   public GameObject SkillCanvas;
    public GameObject MagicSkillTreeCanvas;
    public GameObject AbilitySkillTreeCanvas;

    public ToggleGroup MagicSkillsInstance;

    public GameObject FireBolt;
    public GameObject Inferno;
    public GameObject Nifhelm;
    public GameObject IceBlast;
    public GameObject IceAge;
    public GameObject ThunderSnow;
    public GameObject ElectroCannon;
    public GameObject Supercell;

    public GameObject FireBolt_slot;
    public GameObject Inferno_slot;
    public GameObject Nifhelm_slot;
    public GameObject IceBlast_slot;
    public GameObject IceAge_slot;
    public GameObject ThunderSnow_slot;
    public GameObject ElectroCannon_slot;
    public GameObject Supercell_slot;

    public Toggle FireBolt_btn;
    public Toggle Inferno_btn;
    public Toggle Nifhelm_btn;
    public Toggle IceBlast_btn;
    public Toggle IceAge_btn;
    public Toggle ThunderSnow_btn;
    public Toggle ElectroCannon_btn;
    public Toggle Supercell_btn;

    public int Agility = 1;
    public int Mana = 1;
    public int Health = 1;
    public int str = 1;
    public int def = 1;

    public GameObject errorDialogBox;
    public GameObject cover;
    public Text ErrorMsg;
    public Text currMagicTxt;
    public Text currMagicTxtShadow;

    //this is for the number in the input field in between plus n minus btn
    public Text num1Txt;
    public Text num2Txt;
    public Text num3Txt;
    public Text num4Txt;
    public Text num5Txt;

    public Text Stat1Txt;
    public Text Stat2Txt;
    public Text Stat3Txt;
    public Text Stat4Txt;
    public Text Stat5Txt;

    private int num1;
    private int num2;
    private int num3;
    private int num4;
    private int num5;

    private bool isOnMagicSkillTree;
    private bool isOnAbilitySkillTree;
    private bool isOnSkill;

    //for unlock magic
    private bool Fire1;
    private bool Fire2;
    private bool isLockNifhelm;
    private bool Ice1;
    private bool Ice2;
    private bool isLockThunderSnow;
    private bool Elec1;
    private bool Elec2;

	// Use this for initialization
	void Start () {
        
	}
    public Toggle currentSelection
    {
        get { return MagicSkillsInstance.ActiveToggles().FirstOrDefault(); }
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q)) //getkey will cause flickering
        {
            isOnSkill = !isOnSkill;
            SkillCanvas.SetActive(isOnSkill);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOnMagicSkillTree = !isOnMagicSkillTree;
            MagicSkillTreeCanvas.SetActive(isOnMagicSkillTree);
            if (AbilitySkillTreeCanvas.activeSelf)
            {
                cfmCancel();
                AbilitySkillTreeCanvas.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.T)) //Press T to unlock skill
        {
            checkMagicLock();
        }
        updateMagicPointsTxt();
        updateStatsTxt();
	}
    public void updateMagicPointsTxt(){
        currMagicTxt.text = "Current Magic points left: " + GameObject.Find("Player").GetComponent<PlayerLevelSystem>().MagicSkillPoints;
        currMagicTxtShadow.text = "Current Magic points left: " + GameObject.Find("Player").GetComponent<PlayerLevelSystem>().MagicSkillPoints;
    }
    public void updateStatsTxt()
    {
        Stat1Txt.text = "" + Health;
        Stat2Txt.text = "" + Mana;
        Stat3Txt.text = "" + Agility;
        Stat4Txt.text = "" + str;
        Stat5Txt.text = "" + def;
    }
    public void checkMagicLock()
    {
         if (isOnMagicSkillTree) //If he is on the magic skill tree else do nothing
            {
                Debug.Log(currentSelection);
                if (GameObject.Find("Player").GetComponent<PlayerLevelSystem>().MagicSkillPoints >= 1)
                {
                    Debug.Log("No Prob222");
                    if (currentSelection == FireBolt_btn)
                    {
                        Debug.Log("No Prob");
                        if (!Fire1)
                        {
                            Fire1 = true;
                            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().MagicSkillPoints -= 1;
                            FireBolt_slot.SetActive(true);
                            ErrorMsg.text = "Unlock";
                            errorDialogBox.SetActive(true);
                            cover.SetActive(true);
                        }
                        else
                        {
                            ErrorMsg.text = "This magic had already unlock";
                            errorDialogBox.SetActive(true);
                            cover.SetActive(true);
                        }
                    }
                    if (currentSelection == Inferno_btn)
                    {
                        if (!Fire2)
                        {
                            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().MagicSkillPoints -= 1;
                            Inferno_slot.SetActive(true);
                            ErrorMsg.text = "Unlock";
                            errorDialogBox.SetActive(true);
                            cover.SetActive(true);
                        }
                        else
                        {
                            ErrorMsg.text = "This magic had already unlock";
                            errorDialogBox.SetActive(true);
                            cover.SetActive(true);
                        }
                    }
                    if (currentSelection == Nifhelm_btn)
                    {
                        if (!isLockNifhelm)
                        {
                            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().MagicSkillPoints -= 1;
                            Nifhelm_slot.SetActive(true);
                            ErrorMsg.text = "Unlock";
                            errorDialogBox.SetActive(true);
                            cover.SetActive(true);
                        }
                        else
                        {
                            ErrorMsg.text = "This magic had already unlock";
                            errorDialogBox.SetActive(true);
                            cover.SetActive(true);
                        }
                    }
                    if (currentSelection == IceBlast_btn)
                    {
                        if (!Ice1)
                        {
                            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().MagicSkillPoints -= 1;
                            IceBlast_slot.SetActive(true);
                            ErrorMsg.text = "Unlock";
                            errorDialogBox.SetActive(true);
                            cover.SetActive(true);
                        }
                        else
                        {
                            ErrorMsg.text = "This magic had already unlock";
                            errorDialogBox.SetActive(true);
                            cover.SetActive(true);
                        }
                    }
                    if (currentSelection == IceAge_btn)
                    {
                        if (!Ice2)
                        {
                            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().MagicSkillPoints -= 1;
                            IceAge_slot.SetActive(true);
                            ErrorMsg.text = "Unlock";
                            errorDialogBox.SetActive(true);
                            cover.SetActive(true);
                        }
                        else
                        {
                            ErrorMsg.text = "This magic had already unlock";
                            errorDialogBox.SetActive(true);
                            cover.SetActive(true);
                        }
                    }
                    if (currentSelection == ThunderSnow_btn)
                    {
                        if (!isLockThunderSnow)
                        {
                            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().MagicSkillPoints -= 1;
                            ThunderSnow_slot.SetActive(true);
                            ErrorMsg.text = "Unlock";
                            errorDialogBox.SetActive(true);
                            cover.SetActive(true);
                        }
                        else
                        {
                            ErrorMsg.text = "This magic had already unlock";
                            errorDialogBox.SetActive(true);
                            cover.SetActive(true);
                        }
                    }
                    if (currentSelection == ElectroCannon_btn)
                    {
                        if (!Elec1)
                        {
                            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().MagicSkillPoints -= 1;
                            ElectroCannon_slot.SetActive(true);
                            ErrorMsg.text = "Unlock";
                            errorDialogBox.SetActive(true);
                            cover.SetActive(true);
                        }
                        else
                        {
                            ErrorMsg.text = "This magic had already unlock";
                            errorDialogBox.SetActive(true);
                            cover.SetActive(true);
                        }
                    }
                    if (currentSelection == Supercell_btn)
                    {
                        if (!Elec2)
                        {
                            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().MagicSkillPoints -= 1;
                            Supercell_slot.SetActive(true);
                            ErrorMsg.text = "Unlock";
                            errorDialogBox.SetActive(true);
                            cover.SetActive(true);
                        }
                        else
                        {
                            ErrorMsg.text = "This magic had already unlock";
                            errorDialogBox.SetActive(true);
                            cover.SetActive(true);
                        }
                    }
                }
                else
                {
                    ErrorMsg.text = "Not enough Magic skill points";
                    errorDialogBox.SetActive(true);
                    cover.SetActive(true);
                }
            }
        }

    public void plusHealth()
    {
        if (GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints >= 1)
        {
            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints -= 1;
            num1 += 1;
            num1Txt.text = "" + num1;
        }
        else
        {
            Debug.Log("Not enough");
        }
    }
    public void minusHealth()
    {
        if (num1 >= 1)
        {
            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints += 1;
            num1 -= 1;
            num1Txt.text = "" + num1;
        }
        else
        {
            Debug.Log("Error no more");
        }
    }
    public void plusMana()
    {
        if (GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints >= 1)
        {
            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints -= 1;
            num2 += 1;
            num2Txt.text = "" + num2;
        }
        else
        {
            Debug.Log("Not enough");
        }
    }
    public void minusMana()
    {
        if (num2 >= 1)
        {
            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints += 1;
            num2 -= 1;
            num2Txt.text = "" + num2;
        }
        else
        {
            Debug.Log("Error no more");
        }
    }
    public void plusAgility()
    {
        if (GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints >= 1)
        {
            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints -= 1;
            num3 += 1;
            num3Txt.text = "" + num3;
        }
        else
        {
            Debug.Log("Not enough");
        }
    }
    public void minusAgility()
    {
        if (num3 >= 1)
        {
            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints += 1;
            num3 -= 1;
            num3Txt.text = "" + num3;
        }
        else
        {
            Debug.Log("Error no more");
        }
    }
    public void plusStr()
    {
        if (GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints >= 1)
        {
            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints -= 1;
            num4 += 1;
            num4Txt.text = "" + num4;
        }
        else
        {
            Debug.Log("Not enough");
        }
    }
    public void minusStr()
    {
        if (num4 >= 1)
        {
            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints += 1;
            num4 -= 1;
            num4Txt.text = "" + num4;
        }
        else
        {
            Debug.Log("Error no more");
        }
    }
    public void plusDefense()
    {
        if (GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints >= 1)
        {
            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints -= 1;
            num5 += 1;
            num5Txt.text = "" + num5;
        }
        else
        {
            Debug.Log("Not enough");
        }
    }
    public void minusDefense()
    {
        if (num5 >= 1)
        {
            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints += 1;
            num5 -= 1;
            num5Txt.text = "" + num4;
        }
        else
        {
            Debug.Log("Error no more");
        }
    }
    public void cfmAdd()
    {
        if (num1 >= 1)
        {
            Health += num1;
            Debug.Log(Health);
            cfmHealthAdd();
            num1 = 0;
            num1Txt.text = "" + num1;
        }
        if (num2 >= 1)
        {
            Mana += num2;
            Debug.Log(Mana);
            cfmManaAdd();
            num2 = 0;
            num2Txt.text = "" + num2;
        }
        if (num3 >= 1)
        {
            Agility += num3;
            Debug.Log(Agility);
            num3 = 0;
            num3Txt.text = "" + num3;
        }
        if (num4 >= 1)
        {
            str += num4;
            Debug.Log(str);
            num4 = 0;
            num4Txt.text = "" + num4;
        }
        if (num5 >= 1)
        {
            def += num5;
            Debug.Log(def);
            num5 = 0;
            num5Txt.text = "" + num5;
        }

    }
    public void cfmCancel()
    {
        if (num1 >= 1)
        {
            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints += num1;
            Debug.Log(Health);
            num1 = 0;
            num1Txt.text = "" + num1;
        }
        if (num2 >= 1)
        {
            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints += num2;
            Debug.Log(Mana);
            num2 = 0;
            num2Txt.text = "" + num2;
        }
        if (num3 >= 1)
        {
            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints += num3;
            Debug.Log(Agility);
            num3 = 0;
            num3Txt.text = "" + num3;
        }
        if (num4 >= 1)
        {
            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints += num4;
            Debug.Log(str);
            num4 = 0;
            num4Txt.text = "" + num4;
        }
        if (num5 >= 1)
        {
            GameObject.Find("Player").GetComponent<PlayerLevelSystem>().AbilitySkillPoints += num5;
            Debug.Log(def);
            num5 = 0;
            num5Txt.text = "" + num5;
        }
    }
    public void openMagicTree(){
        Debug.Log(isOnMagicSkillTree);
        MagicSkillTreeCanvas.SetActive(isOnMagicSkillTree);
        AbilitySkillTreeCanvas.SetActive(false);
    }
    public void openAbilityTree(){
        AbilitySkillTreeCanvas.SetActive(true);
        MagicSkillTreeCanvas.SetActive(!isOnMagicSkillTree);
    }
    public void OnFireBolt()
    {
        FireBolt.SetActive(true);
        Inferno.SetActive(false);
        Nifhelm.SetActive(false);
        IceAge.SetActive(false);
        IceBlast.SetActive(false);
        ThunderSnow.SetActive(false);
        ElectroCannon.SetActive(false);
        Supercell.SetActive(false);
    }
    public void OnInferno()
    {
        FireBolt.SetActive(false);
        Inferno.SetActive(true);
        Nifhelm.SetActive(false);
        IceAge.SetActive(false);
        IceBlast.SetActive(false);
        ThunderSnow.SetActive(false);
        ElectroCannon.SetActive(false);
        Supercell.SetActive(false);
    }
    public void OnNifhelm()
    {
        FireBolt.SetActive(false);
        Inferno.SetActive(false);
        Nifhelm.SetActive(true);
        IceAge.SetActive(false);
        IceBlast.SetActive(false);
        ThunderSnow.SetActive(false);
        ElectroCannon.SetActive(false);
        Supercell.SetActive(false);
    }
    public void OnIceBlast()
    {
        FireBolt.SetActive(false);
        Inferno.SetActive(false);
        Nifhelm.SetActive(false);
        IceAge.SetActive(false);
        IceBlast.SetActive(true);
        ThunderSnow.SetActive(false);
        ElectroCannon.SetActive(false);
        Supercell.SetActive(false);
    }
    public void OnIceAge()
    {
        FireBolt.SetActive(false);
        Inferno.SetActive(false);
        Nifhelm.SetActive(false);
        IceAge.SetActive(true);
        IceBlast.SetActive(false);
        ThunderSnow.SetActive(false);
        ElectroCannon.SetActive(false);
        Supercell.SetActive(false);
    }
    public void OnThundersnow()
    {
        FireBolt.SetActive(false);
        Inferno.SetActive(false);
        Nifhelm.SetActive(false);
        IceAge.SetActive(false);
        IceBlast.SetActive(false);
        ThunderSnow.SetActive(true);
        ElectroCannon.SetActive(false);
        Supercell.SetActive(false);
    }
    public void OnElcetroCannon()
    {
        FireBolt.SetActive(false);
        Inferno.SetActive(false);
        Nifhelm.SetActive(false);
        IceAge.SetActive(false);
        IceBlast.SetActive(false);
        ThunderSnow.SetActive(false);
        ElectroCannon.SetActive(true);
        Supercell.SetActive(false);
    }
    public void OnSupercell()
    {
        FireBolt.SetActive(false);
        Inferno.SetActive(false);
        Nifhelm.SetActive(false);
        IceAge.SetActive(false);
        IceBlast.SetActive(false);
        ThunderSnow.SetActive(false);
        ElectroCannon.SetActive(false);
        Supercell.SetActive(true);
    }
    public void closedDialog()
    {
        errorDialogBox.SetActive(false);
        cover.SetActive(false);
    }
    void cfmManaAdd() //To give more mana
    {
        GameObject.Find("Player").GetComponent<PlayerMana>().maxMana += ((float)Mana-1.0f)/10.0f;
        Debug.Log(GameObject.Find("Player").GetComponent<PlayerMana>().maxMana);
    }
    void cfmHealthAdd() //To give more health
    {
        GameObject.Find("Player").GetComponent<PlayerHealth>().maxHealth += ((float)Health - 1.0f) / 10.0f;
        Debug.Log(GameObject.Find("Player").GetComponent<PlayerHealth>().maxHealth);
    }*/
}
