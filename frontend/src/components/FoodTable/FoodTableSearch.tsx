import { TextField } from "@mui/material";

interface FoodTableSearchProps {
  onSearch: (searchText: string) => void;
}

function FoodTableSearch({ onSearch }: FoodTableSearchProps) {
  return (
    <TextField
      label="Search food"
      size="small"
      fullWidth
      onChange={(e) => onSearch(e.target.value.toLowerCase())}
      sx={{ width: "50%", margin: 2, alignSelf: "start" }}
    />
  );
}

export default FoodTableSearch;
