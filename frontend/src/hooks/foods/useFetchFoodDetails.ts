import { useEffect, useState } from "react";
import { FoodDetails } from "../../interfaces/FoodDetails";
import { useParams } from "react-router-dom";
import { fetchDetails } from "../../services/foods/foodDetailsService";

export const useFetchFoodDetails = () => {
  const { code } = useParams<{ code?: string }>();
  const [foodDetails, setFoodDetails] = useState<FoodDetails | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchFoodDetails = async () => {
      if (!code) {
        setError("Invalid code provided.");
        setLoading(false);
        return;
      }

      try {
        const response = await fetchDetails(code);
        if (!response || !response.data) {
          throw new Error("Invalid response format.");
        }
        setFoodDetails(response);
      } catch (error) {
        setError("Error fetching food details.");
        console.error("Error fetching food details:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchFoodDetails();
  }, [code]);

  return { foodDetails, loading, error };
};
