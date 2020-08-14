using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ModEditor : MonoBehaviour
{
    public ModObject target;

    public Text Titlebar;
    public Image TypeBar;
    public Image CategoryBar;

    public string SelectedType;
    public string SelectedCat;

    public GameObject BuyMenu;
    public Text PriceText;

    public GameObject ExitScreen;

    List<ModButton> TypeButtons = new List<ModButton>();
    public RectTransform PartView;
    List<ModButton> PartViewButtons = new List<ModButton>();
    List<ModButton> CatButtons = new List<ModButton>();

    public VehicleStatView StatView;
    void Start()
    {
    }
    public int CalculatePrice()
    {
        int price=0;
        foreach (var item in target.GetInstalledParts())
        {
            if (!SaveSystem.GetOwnedPartsForVehicleType(target.Name).Contains(item))
            {
                price += ModSystemTools.GetPart(item).price;
            }
        }
        return price;
    }
    public void ChangeType(string type)
    {
        try
        {
            TypeButtons.First(x => x.Name == SelectedType).Deselect();
        }
        catch { }
        TypeButtons.First(x => x.Name == type).Select();
        SelectedType = type;
        PreparePartButtons();
    }
    public void ChangePart(string part)
    {
        
        if (part=="null")
        {
            try
            {
                PartViewButtons.First(x => x.Name == target.GetInstalledPart(SelectedType)).Deselect();
            }
            catch
            {
            }
            target.RemovePart(SelectedType);
            StatView.ShowValues(target);
            if (CalculatePrice() > 0)
            {
                BuyMenu.SetActive(true);
                PriceText.text = "$ " + CalculatePrice();
            }
            else
            {
                BuyMenu.SetActive(false);
            }
            PartViewButtons.First(x => x.Name=="null").Select();
            return;
        }
        try
        {
            PartViewButtons.First(x => x.Name == target.GetInstalledPart(SelectedType)).Deselect();
        }
        catch {
        }
        PartViewButtons.First(x => x.Name == part).Select();

        target.AttachPart(part);
        StatView.ShowValues(target);
        if (CalculatePrice() > 0)
        {
            BuyMenu.SetActive(true);
            PriceText.text = "$ " + CalculatePrice();
        }
        else
        {
            BuyMenu.SetActive(false);
        }
        
    }
    public void ChangeCategory(string cat)
    {
        try
        {
            CatButtons.First(x => x.Name == SelectedCat).Deselect();
        }
        catch { }
        try
        {
            CatButtons.FirstOrDefault(x => x.Name == cat).Select();
        }
        catch { }
        SelectedCat = cat;
        PrepareTypeButtons();
    }

    public void BuyButton()
    {
        if (CalculatePrice()<=SaveSystem.GetCash())
        {
            SaveSystem.AddCash(-CalculatePrice());
            SaveSystem.AddLog(new Save.LogEntry("ModEditor", $"Bought parts for {target.Name}, $ {CalculatePrice()}"));
            GarageManager.instance.SaveEditedVehicleWithoutExiting();
            BuyMenu.SetActive(false);
        }
    }

    public void ReturnBtn()
    {
        if (CalculatePrice()>0)
        {
            ExitScreen.SetActive(true);
            return;
        }
        GarageManager.instance.SaveEditedVehicle();
    }

    // Update is called once per frame
    public async Task Reload(ModObject v)
    {
        await ModSystemTools.LoadPartsAsync();
        target = v;
        Titlebar.text = $"Modify {TranslationSystem.GetString("CAR_"+target.Name)}";
        PrepareCatButtons();
        PrepareTypeButtons();
        StatView.ShowValues(target);
    }

    private void PrepareTypeButtons()
    {
        if (TypeButtons.Count>0)
        {
            foreach (var item in TypeButtons)
            {
                Destroy(item.gameObject);
            }
            TypeButtons = new List<ModButton>();
        }
        GameObject buttonPrefab = Resources.Load<GameObject>("ModSystem/UI/Button");

        foreach (var item in target.GetCompatiblePartTypes(SelectedCat))
        {
            var gm = Instantiate(buttonPrefab, TypeBar.transform);

            var btn = gm.GetComponent<Button>();
            btn.onClick.AddListener(delegate { ChangeType(item); });

            var modbtn = gm.GetComponent<ModButton>();
            modbtn.TranslationName = $"TYPE_{item}";
            modbtn.Name = item;

            TypeButtons.Add(modbtn);
        }
        try
        {
            ChangeType(TypeButtons[0].Name);
        }
        catch { }
    }
    private void PreparePartButtons()
    {
        if (PartViewButtons.Count > 0)
        {
            foreach (var item in PartViewButtons)
            {
                Destroy(item.gameObject);
            }
            PartViewButtons = new List<ModButton>();
        }
        GameObject buttonPrefab = Resources.Load<GameObject>("ModSystem/UI/Button");
        var items = ModSystemTools.GetCompatibleParts(target.Name, SelectedType);
        items.Remove("null");
        items=items.OrderBy(x=> ModSystemTools.GetPart(x).price).ToList();
        items.Insert(0,"null");
        foreach (var item in items)
        {
            if (!ModSystemTools.IsTypeNullable(SelectedType)&&item=="null")
            {
                continue;
            }
            var gm = Instantiate(buttonPrefab, PartView);

            var btn = gm.GetComponent<Button>();
            btn.onClick.AddListener(delegate { ChangePart(item); });

            var modbtn = gm.GetComponent<ModButton>();
            modbtn.Name = item;
            modbtn.TranslationName = $"PART_{item}";
            if (item!="null")
            {
                if (SaveSystem.GetOwnedPartsForVehicleType(target.Name).Contains(item))
                {
                    modbtn.ChangeStatusText("Owned");
                }
                else
                {
                    modbtn.ChangeStatusText(ModSystemTools.GetPart(item).price.ToString());
                }
            }
            
            PartViewButtons.Add(modbtn);
        }
        ChangePart(target.GetInstalledPart(SelectedType));
    }
    private void PrepareCatButtons()
    {
        if (CatButtons.Count > 0)
        {
            foreach (var item in CatButtons)
            {
                Destroy(item.gameObject);
            }
            CatButtons = new List<ModButton>();
        }
        GameObject buttonPrefab = Resources.Load<GameObject>("ModSystem/UI/Button");

        foreach (var item in target.GetCompatibleCategories())
        {
            var gm = Instantiate(buttonPrefab, CategoryBar.GetComponent<RectTransform>());

            var btn = gm.GetComponent<Button>();
            btn.onClick.AddListener(delegate { ChangeCategory(item); });

            var modbtn = gm.GetComponent<ModButton>();
            modbtn.Name = item;
            CatButtons.Add(modbtn);
        }
        ChangeCategory(CatButtons[0].name);
    }
}
