import { API_URL_FOOD } from "../../config";

export const fetchFoods = async () => {
  const response = await fetch(API_URL_FOOD);
  const result = await response.json();
  if (result.success) {
    return result.data;
  } else {
    throw new Error("Failed to fetch foods");
  }
};
