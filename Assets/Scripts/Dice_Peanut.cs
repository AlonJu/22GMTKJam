using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Peanut : MonoBehaviour
{
   
    public Transform self; 
    public string diceType = "White";
    public int sideIndex;
    public int returnNumber;
    public int realIndex;
    float[] angles = new float[6];
    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        realIndex = CheckSides();
        returnNumber = finalNumber(realIndex);
    }
    public int finalNumber(int sIndex){
        switch(diceType){
            /*case "Red":
                if (sideIndex % 2 == 0){
                    return sideIndex;
                } else{
                return 0;
                }
            case "Black":
                if (sideIndex > 3){
                return 6;
                } else{
                return 3;
                }*/
            case "White":
            return sIndex;
        }
        return 0;
    }
    public int CheckSides() // calculates which side is closest to up, and then returns it's index
    {
        
        angles[0] = Vector3.Angle(Vector3.up, -self.up);
        angles[1] = Vector3.Angle(Vector3.up, -self.forward); 
        angles[2] = Vector3.Angle(Vector3.up, -self.right);
        angles[3] = Vector3.Angle(Vector3.up, self.right);
        angles[4] = Vector3.Angle(Vector3.up, self.forward); 
        angles[5] = Vector3.Angle(Vector3.up, self.up); 
        float [] tempAngles = new float[6]; // ugly list is necessary
        for (int i = 0; i < angles.Length; i++){
            tempAngles[i] = angles[i];
        } //c# moment
        
        SelectionSort(tempAngles); // i wrote this myself (im very smart)

        for (int i = 0; i < angles.Length; i++){ // i have to do this because after the list is sorted its impossible to know which side it came from
                sideIndex = i+1;
                //Debug.Log(angles[sideIndex]);
                
            if (angles[i] == tempAngles[0]){
                realIndex=i+1;
                return sideIndex;
            } 
        }
        return sideIndex;
    }
        public void SelectionSort(float[] nums)
    {
        for (int i = 0; i < nums.Length - 1; i++)
        {
            int smallestNumIndex = i;
            for (int j = i + 1; j < nums.Length; j++)
            {
                if(nums[j]<nums[smallestNumIndex])
                    smallestNumIndex = j;
            }
            if(smallestNumIndex != i)
            {
                float temp = nums[i];
                nums[i] = nums[smallestNumIndex];
                nums[smallestNumIndex] = temp;
            }
        }
    }
}
