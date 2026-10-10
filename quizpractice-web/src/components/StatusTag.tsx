import { Tag } from 'antd'

// One colour per status value of BR-23, so a status looks the same on every screen.
const COLOURS: Record<string, string> = {
  Shown: 'green',
  Hidden: 'default',
  Active: 'green',
  Inactive: 'default',
  Unverified: 'gold',
  Published: 'green',
  Unpublished: 'default',
  Submitted: 'blue',
  Paid: 'green',
  Cancelled: 'red',
  Unpaid: 'gold',
  'In Progress': 'blue',
}

export function StatusTag({ status }: { status: string }) {
  return <Tag color={COLOURS[status] ?? 'default'}>{status}</Tag>
}
