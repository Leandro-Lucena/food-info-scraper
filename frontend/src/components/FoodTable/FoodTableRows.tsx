import { DataGrid, GridColDef, GridEventListener } from "@mui/x-data-grid";
import { Food } from "../../interfaces/Food";

interface FoodTableRowsProps {
  rows: Food[];

  columns: GridColDef[];

  loading: boolean;

  onRowClick: GridEventListener<"rowClick">;
}

const paginationModel = { page: 0, pageSize: 15 };

function FoodTableRows({
  rows,
  columns,
  loading,
  onRowClick,
}: FoodTableRowsProps) {
  return (
    <DataGrid
      getRowId={(row) => row.code}
      loading={loading}
      disableColumnMenu
      disableColumnSelector
      disableRowSelectionOnClick
      rows={rows}
      rowHeight={50}
      columns={columns}
      initialState={{ pagination: { paginationModel } }}
      pageSizeOptions={[15, 20, 50, 100]}
      onRowClick={onRowClick}
      getRowClassName={(params) =>
        params.indexRelativeToCurrentPage % 2 === 0 ? "even-row" : "odd-row"
      }
      sx={(theme) => ({
        border: 0,
        "& .MuiDataGrid-row:hover": {
          cursor: "pointer",
          backgroundColor: theme.palette.action.hover,
        },
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
  );
}

export default FoodTableRows;
