import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { FoodDetails } from "../../interfaces/FoodDetails";
import { Paper } from "@mui/material";

interface DetailsTableProps {
  foodDetails: FoodDetails;
}

function DetailsTable({ foodDetails }: DetailsTableProps) {
  if (!foodDetails.data || !Array.isArray(foodDetails.data)) {
    return <div>No details available.</div>;
  }

  const columns: GridColDef[] = [
    { field: "component", headerName: "Component", flex: 2 },
    { field: "valuePer100g", headerName: "Value (per 100g)", flex: 1 },
    { field: "units", headerName: "Units", flex: 1 },
    { field: "standardDeviation", headerName: "Std Deviation", flex: 1 },
    { field: "minValue", headerName: "Min Value", flex: 1 },
    { field: "maxValue", headerName: "Max Value", flex: 1 },
    { field: "dataCount", headerName: "Data Count", flex: 1 },
    { field: "foodReferences", headerName: "References", flex: 1 },
    { field: "dataType", headerName: "Data Type", flex: 1 },
  ];

  const rows = foodDetails.data.map((nutrient, index) => ({
    id: index,
    component: nutrient.component,
    valuePer100g: nutrient.valuePer100g,
    units: nutrient.units,
    standardDeviation: nutrient.standardDeviation,
    minValue: nutrient.minValue,
    maxValue: nutrient.maxValue,
    dataCount: nutrient.dataCount,
    foodReferences: nutrient.foodReferences,
    dataType: nutrient.dataType,
  }));

  return (
    <Paper sx={{ width: "100%" }} elevation={5}>
      <DataGrid
        rows={rows}
        columns={columns}
        disableColumnMenu
        disableColumnSelector
        disableRowSelectionOnClick
        hideFooter
        rowHeight={38}
        initialState={{
          sorting: {
            sortModel: [{ field: "component", sort: "asc" }],
          },
        }}
        getRowClassName={(params) =>
          params.indexRelativeToCurrentPage % 2 === 0 ? "even-row" : "odd-row"
        }
        sx={(theme) => ({
          border: 0,
          "& .even-row": {
            backgroundColor:
              theme.palette.mode === "dark" ? "#2b2b2b" : "#f9f9f9",
          },
          "& .odd-row": {
            backgroundColor:
              theme.palette.mode === "dark" ? "#1e1e1e" : "#ffffff",
          },
        })}
      />
    </Paper>
  );
}

export default DetailsTable;
