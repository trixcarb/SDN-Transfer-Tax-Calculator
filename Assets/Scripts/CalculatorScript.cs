using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;   
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Globalization;

public class CalculatorScript : MonoBehaviour
{
    public Text TransferTaxTextCopy;
    public Text PenaltyTextCopy;
    public Text ResearchFeeQtyTextCopy;
    public Text TotalText;
    public Text NumberofDaysText;
    public Text NumberofMonthsText;
    public Text NumberofYearsText;
    public Text ErrorAmountText;
    public Text ErrorDateText;

    public InputField HigherAmount;
    public InputField DateNotarized;
    public InputField CurrentDate;
    public InputField ResearchFee;

    public float HigherAmountFloat;
    public float ResearchFeeFloat;
    public float TotalTextFloat;

    public string DateNotarizedString;
    public string CurrentDateString;

    public DateTime DateNotarizedTime;
    public DateTime CurrentDateTime;

    // Start is called before the first frame update
    void Start()
    {
        TransferTaxTextCopy.text = "0.00";
        PenaltyTextCopy.text = "0.00";
        ResearchFeeQtyTextCopy.text = "0.00";
        TotalText.text = "0.00";
        NumberofDaysText.text = "0";
        NumberofMonthsText.text= "0";
        NumberofYearsText.text = "0";

        HigherAmount.text = "";
        DateNotarized.text = "";
        CurrentDate.text = "";

        CurrentDateTime = DateTime.Now;
        CurrentDate.text = CurrentDateTime.ToString("MM/dd/yyyy");
        
        ResearchFee.text = "";


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetHigherAmount()
    {
        HigherAmountFloat = float.Parse(HigherAmount.text);
    }
    public void GetResearchFee()
    {
        ResearchFeeFloat = float.Parse(ResearchFee.text);
    }
    public void GetNotarizedDate()
    {
        DateNotarizedString = DateNotarized.text;
    }
    public void GetCurrentDate()
    {
        CurrentDateString = CurrentDate.text;
    }

    public void CalculateButton()
    {
        if(HigherAmount.text == "")
        {
            ErrorAmountText.text = "Error: No Amount";
        }
        if(DateNotarized.text != "" && HigherAmount.text != "")
        {
            string inputText = DateNotarized.text;
            DateTime parsedDate;

                if (DateTime.TryParseExact(inputText, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                        GetNotarizedDate();
                        GetCurrentDate();

                        DateTime DateNotarizedTime = DateTime.ParseExact(DateNotarizedString, "MM/dd/yyyy", null);
                        DateTime CurrentDateTime = DateTime.ParseExact(CurrentDateString, "MM/dd/yyyy", null);

                        TimeSpan timeDifference = CurrentDateTime - DateNotarizedTime;
                        int daysCount = timeDifference.Days;
                        int monthsCount = daysCount / 30;
                        int yearsCount = monthsCount / 12;

                        float PenaltyPercentage = monthsCount * 0.02f;
                                    
                        Debug.Log("Number of days: " + daysCount);
                        Debug.Log("Number of months: " + monthsCount);

                        NumberofDaysText.text = daysCount.ToString();
                        NumberofMonthsText.text = monthsCount.ToString();
                        NumberofYearsText.text = yearsCount.ToString();

                        //Calculating Transfer Tax Fee
                        GetHigherAmount();
                        float HigherAmountResult = (HigherAmountFloat * 0.01f) * 0.5f;
                        TransferTaxTextCopy.text = HigherAmountResult.ToString("F2");

                        //Calculating Research Fee and its quantity, ResearchFeeFloat is the one to be add for the overall Total
                        GetResearchFee();
                        float ResearchFeeResult = ResearchFeeFloat / 50f;
                        ResearchFeeQtyTextCopy.text = ResearchFeeResult.ToString();

                        if(daysCount <= 59)
                        {
                        //Calculating Penalty Assuming 3yrs
                        float PenaltyResultTY = HigherAmountResult * 0;
                        PenaltyTextCopy.text =  PenaltyResultTY.ToString("F2");

                        //Calculating Total Payment
                        TotalTextFloat = HigherAmountResult + PenaltyResultTY + ResearchFeeFloat;
                        TotalText.text = TotalTextFloat.ToString("F2");
                        }
                        else if(daysCount >= 1095)
                        {
                        //Calculating Penalty Assuming 3yrs
                        float PenaltyResultTY = HigherAmountResult * 0.72f;
                        PenaltyTextCopy.text =  PenaltyResultTY.ToString("F2");

                        //Calculating Total Payment
                        TotalTextFloat = HigherAmountResult + PenaltyResultTY + ResearchFeeFloat;
                        TotalText.text = TotalTextFloat.ToString("F2");    
                        }
                        else if(daysCount > 59 && daysCount < 1095)
                        {
                        //Calculating Penalty Assuming 3yrs
                        float PenaltyResultTY = HigherAmountResult * PenaltyPercentage;
                        PenaltyTextCopy.text =  PenaltyResultTY.ToString("F2");

                        //Calculating Total Payment
                        TotalTextFloat = HigherAmountResult + PenaltyResultTY + ResearchFeeFloat;
                        TotalText.text = TotalTextFloat.ToString("F2");    
                        }
                        ErrorAmountText.text = "";
                        ErrorDateText.text = "";

                //ErrorDateText.text = "Valid Date: " + parsedDate.ToString("MM/dd/yyyy");
                }
                else
                {
                ErrorDateText.text = "Invalid Date Format";
                ErrorAmountText.text = "";
                }
    }

        if(DateNotarized.text == "" && HigherAmount.text != "")
        {
            //Calculating Transfer Tax Fee
            GetHigherAmount();
            float HigherAmountResult = (HigherAmountFloat * 0.01f) * 0.5f;
            TransferTaxTextCopy.text = HigherAmountResult.ToString("F2");

            //Calculating Research Fee and its quantity, ResearchFeeFloat is the one to be add for the overall Total
            GetResearchFee();
            float ResearchFeeResult = ResearchFeeFloat / 50f;
            ResearchFeeQtyTextCopy.text = ResearchFeeResult.ToString();

            //Calculating Penalty Assuming 3yrs
            float PenaltyResultTY = HigherAmountResult * 0.72f;
            PenaltyTextCopy.text =  PenaltyResultTY.ToString("F2");

            //Calculating Total Payment
            TotalTextFloat = HigherAmountResult + PenaltyResultTY + ResearchFeeFloat;
            TotalText.text = TotalTextFloat.ToString("F2");
            ErrorAmountText.text = "";
            ErrorDateText.text = "";
        }

    }

    public void ClearButton()
    {
        TransferTaxTextCopy.text = "0.00";
        PenaltyTextCopy.text = "0.00";
        ResearchFeeQtyTextCopy.text = "0.00";
        TotalText.text = "0.00";
        NumberofDaysText.text = "0";
        NumberofMonthsText.text= "0";
        NumberofYearsText.text = "0";

        HigherAmount.text = "";
        DateNotarized.text = "";
        ErrorAmountText.text = "";
        ErrorDateText.text = "";

        CurrentDateTime = DateTime.Now;
        CurrentDate.text = CurrentDateTime.ToString("MM/dd/yyyy");
        ResearchFee.text = "";
        

        HigherAmountFloat = 0;
        ResearchFeeFloat = 0;
        TotalTextFloat = 0;
        
    }
    public void CopyTransferTaxFee()
    {
        TextEditor textEditor = new TextEditor();
        textEditor.text = TransferTaxTextCopy.text;
        textEditor.SelectAll();
        textEditor.Copy();
    }
    public void CopyPenalty()
    {
        TextEditor textEditor1 = new TextEditor();
        textEditor1.text = PenaltyTextCopy.text;
        textEditor1.SelectAll();
        textEditor1.Copy();
    }
    public void CopyResearchFeeQty()
    {
        TextEditor textEditor2 = new TextEditor();
        textEditor2.text = ResearchFeeQtyTextCopy.text;
        textEditor2.SelectAll();
        textEditor2.Copy();
    }
    //redirect url
    public void LogoURL()
    {
        Application.OpenURL("https://surigaodelnorte.gov.ph");
    }
        public void Logo2URL()
    {
        Application.OpenURL("https://cydy.dev");
    }
    public void DateTimeTest()
    {
        string inputText = DateNotarized.text;
        DateTime parsedDate;

        if (DateTime.TryParseExact(inputText, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
        {
            ErrorDateText.text = "Valid Date: " + parsedDate.ToString("MM/dd/yyyy");
        }
        else
        {
            ErrorDateText.text = "Invalid Date Format";
        }
    
    }
    
}
