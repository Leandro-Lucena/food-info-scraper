export interface FoodDetails {
  success: boolean;
  message: string;
  foodName: string;
  data: Nutrient[];
}
interface Nutrient {
  component: string;
  units: string;
  valuePer100g: number;
  standardDeviation: number;
  minValue: number;
  maxValue: number;
  dataCount: number;
  foodReferences: string;
  dataType: string;
}
