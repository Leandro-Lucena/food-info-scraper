import { useEffect, useState } from "react";
import { GridColDef, GridEventListener } from "@mui/x-data-grid";
import Paper from "@mui/material/Paper";
import FoodTableSearch from "./FoodTableSearch";
import FoodTableRows from "./FoodTableRows";
import { useFetchFoods } from "../../hooks/foods/useFetchFoods";
import { debounce } from "lodash";
import { Food } from "../../interfaces/Food";
import { useNavigate } from "react-router-dom";

const columns: GridColDef[] = [
  { field: "code", headerName: "Code", flex: 1 },
  { field: "name", headerName: "Name", flex: 5 },
  { field: "scientificName", headerName: "Cientific Name", flex: 1.5 },
  { field: "foodGroup", headerName: "Food Group", flex: 1.5 },
];

function FoodTable() {
  const navigate = useNavigate();
  const { data: foods, loading } = useFetchFoods();
  const [filteredRows, setFilteredRows] = useState<Food[]>([]);

  useEffect(() => {
    setFilteredRows(foods);
  }, [foods]);

  const removeDiacritics = (text: string) => {
    return text.normalize("NFD").replace(/[\u0300-\u036f]/g, "");
  };

  const handleSearch = debounce((searchText: string) => {
    const searchLower = removeDiacritics(searchText.toLowerCase());

    const startsWithMatches = foods.filter(
      (row) =>
        removeDiacritics(row.name.toLowerCase()).startsWith(searchLower) ||
        removeDiacritics(row.scientificName.toLowerCase()).startsWith(
          searchLower
        )
    );

    const containsMatches = foods.filter((row) => {
      const rowName = removeDiacritics(row.name.toLowerCase());
      const rowScientificName = removeDiacritics(
        row.scientificName.toLowerCase()
      );

      const containsMatch =
        rowName.includes(searchLower) ||
        rowScientificName.includes(searchLower);

      const startsWithMatch =
        rowName.startsWith(searchLower) ||
        rowScientificName.startsWith(searchLower);

      return containsMatch && !startsWithMatch;
    });

    setFilteredRows([...startsWithMatches, ...containsMatches]);
  }, 500);

  const handleRowClick: GridEventListener<"rowClick"> = (params) => {
    const code = params.id;
    navigate(`/food/${code}`);
  };

  return (
    <div
      style={{
        flexDirection: "column",
        alignItems: "center",
      }}
    >
      <FoodTableSearch onSearch={handleSearch} />
      <Paper elevation={5}>
        <FoodTableRows
          rows={filteredRows}
          columns={columns}
          loading={loading}
          onRowClick={handleRowClick}
        />
      </Paper>
    </div>
  );
}

export default FoodTable;
