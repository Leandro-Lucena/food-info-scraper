import { useFetchFoodDetails } from "../hooks/foods/useFetchFoodDetails";
import DetailsTable from "../components/DetailsTable/DetailsTable";
import { Button, CircularProgress, Divider, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";

function FoodDetailsPage() {
  const navigate = useNavigate();
  const { foodDetails, loading, error } = useFetchFoodDetails();

  if (loading)
    return (
      <div
        style={{
          width: "100vw",
          height: "100vh",
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        <CircularProgress />
      </div>
    );
  if (error) return <div>{error}</div>;
  if (!foodDetails || !foodDetails.data || !Array.isArray(foodDetails.data)) {
    return <div>No details available.</div>;
  }

  const handleGoBack = () => {
    navigate(-1);
  };

  return (
    <div
      style={{
        paddingBottom: 20,
        margin: "0 auto",
        width: "80%",
        display: "flex",
        gap: 5,
        flexDirection: "column",
      }}
    >
      <Typography
        variant="h4"
        sx={{
          padding: 2,
          textAlign: "start",
        }}
      >
        {foodDetails.foodName}
      </Typography>
      <Divider variant="middle" />
      <Typography
        variant="h6"
        sx={{
          paddingX: 2,
          width: "100%",
          position: "relative",
          marginY: 1,
        }}
      >
        <Button
          variant="contained"
          color="primary"
          onClick={handleGoBack}
          sx={{
            position: "absolute",
            bottom: -2,
            right: 20,
          }}
        >
          Back
        </Button>
        Nutritional Information
      </Typography>
      <DetailsTable foodDetails={foodDetails} />
    </div>
  );
}

export default FoodDetailsPage;
