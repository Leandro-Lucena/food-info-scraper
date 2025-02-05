import { API_URL_SCRAPER } from "../../config";

export const updateTables = async () => {
  try {
    const response = await fetch(API_URL_SCRAPER);
    if (response.ok) {
      const result = await response.json();
      return result.message;
    } else if (response.status === 204) {
      return "No new data to update.";
    }
    const errorData = await response.json();
    throw new Error(errorData?.message || "Failed to update tables");
  } catch (error) {
    throw error;
  }
};
