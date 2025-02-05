import { useState, useEffect } from "react";
import { Food } from "../../interfaces/Food";
import { fetchFoods } from "../../services/foods/foodService";

export const useFetchFoods = () => {
  const [data, setData] = useState<Food[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetch = async () => {
      setLoading(true);
      try {
        const result = await fetchFoods();
        setData(result);
      } catch (error) {
        console.error("Error fetching foods:", error);
      } finally {
        setLoading(false);
      }
    };

    fetch();
  }, []);

  return { data, loading };
};
