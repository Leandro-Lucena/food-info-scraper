import { API_URL_FOOD } from "../../config";

export const fetchDetails = async (code: string) => {
  if (!code) {
    throw new Error("Invalid code provided.");
  }

  try {
    const response = await fetch(`${API_URL_FOOD}/${code}`);

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }

    const result = await response.json();

    if (!result || typeof result !== "object") {
      throw new Error("Unexpected response format.");
    }

    if (result.success && result.data) {
      return result;
    } else {
      throw new Error(result.message || "Failed to fetch food details.");
    }
  } catch (error) {
    console.error("Error fetching food details:", error);
    throw new Error("Error fetching food details.");
  }
};
