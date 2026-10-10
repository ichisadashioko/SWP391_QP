import { Button, Empty, Table } from 'antd'
import type { TableColumnsType, TableProps } from 'antd'
import type { SortOrder } from 'antd/es/table/interface'
import type { ListCriteria, Page } from '../api/types'

export interface DataTableColumn<T> {
  title: string
  /** Field of the row to show; also the sortBy value sent to the API when sortable. */
  key: string
  sortable?: boolean
  width?: number
  render?: (row: T) => React.ReactNode
}

interface DataTableProps<T> {
  columns: DataTableColumn<T>[]
  page: Page<T> | undefined
  loading: boolean
  criteria: ListCriteria
  /** Page or order changed; the screen writes it into the address (useListParams). */
  onChange: (changes: { page?: number; sortBy?: string; descending?: boolean }) => void
  /** Empty result with criteria applied: offer to clear them (SRS III 0.3). */
  hasCriteria: boolean
  onClearCriteria: () => void
  rowKey?: keyof T & string
}

/**
 * The list table of every list screen (SRS III 0.3), built on the Ant Design Table so
 * all 45 screens page, sort and say "no result" the same way:
 * - ten rows per page and the position "1-10 of 46";
 * - a heading sorts on the server; selecting it again reverses the order (Ant Design
 *   would otherwise add a third, unsorted state);
 * - an empty result keeps the criteria and offers to clear them.
 */
export function DataTable<T extends object>({
  columns,
  page,
  loading,
  criteria,
  onChange,
  hasCriteria,
  onClearCriteria,
  rowKey = 'id' as keyof T & string,
}: DataTableProps<T>) {
  const antColumns: TableColumnsType<T> = columns.map((c) => ({
    title: c.title,
    key: c.key,
    dataIndex: c.key,
    width: c.width,
    render: c.render ? (_: unknown, row: T) => c.render!(row) : undefined,
    sorter: c.sortable,
    sortDirections: c.sortable ? (['ascend', 'descend', 'ascend'] as SortOrder[]) : undefined,
    sortOrder: c.sortable && criteria.sortBy === c.key ? (criteria.descending ? 'descend' : 'ascend') : null,
  }))

  const handleChange: TableProps<T>['onChange'] = (pagination, _filters, sorter) => {
    const s = Array.isArray(sorter) ? sorter[0] : sorter
    const sortBy = s?.order ? String(s.columnKey) : undefined
    const descending = s?.order === 'descend'
    const sortChanged = sortBy !== criteria.sortBy || descending !== Boolean(criteria.descending)
    onChange(sortChanged ? { sortBy, descending } : { page: pagination.current })
  }

  return (
    <Table<T>
      rowKey={rowKey}
      columns={antColumns}
      dataSource={page?.items}
      loading={loading}
      onChange={handleChange}
      pagination={{
        current: criteria.page,
        pageSize: criteria.pageSize,
        total: page?.totalCount ?? 0,
        showSizeChanger: false,
        showTotal: (total, [from, to]) => `${from}-${to} of ${total}`,
      }}
      locale={{
        emptyText: (
          <Empty description={hasCriteria ? 'No record matches the criteria.' : 'Nothing here yet.'}>
            {hasCriteria && <Button onClick={onClearCriteria}>Clear criteria</Button>}
          </Empty>
        ),
      }}
    />
  )
}
