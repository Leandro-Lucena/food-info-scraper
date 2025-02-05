import { useState } from "react";
import { updateTables } from "../../services/scraper/scraperService"; // Certifique-se de que esse serviço exista

export const useUpdateFoods = () => {
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);

  const updateFoods = async () => {
    setLoading(true);
    setMessage(null);
    setError(null);

    try {
      console.log("chamou updateFoods()");
      const result = await updateTables();
      setMessage(result);
    } catch (err) {
      if (err instanceof Error) {
        setError(err.message);
      } else {
        setError("An unknown error occurred");
      }
    } finally {
      setLoading(false);
    }
  };

  return { updateFoods, loading, message, error };
};
