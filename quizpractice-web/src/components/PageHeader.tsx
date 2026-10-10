import { Flex, Typography } from 'antd'

/** Page Title of SRS III 0.1: name of the screen, with its main action on the right. */
export function PageHeader({ title, action }: { title: string; action?: React.ReactNode }) {
  return (
    <Flex justify="space-between" align="center" style={{ marginBottom: 16 }}>
      <Typography.Title level={3} style={{ margin: 0 }}>
        {title}
      </Typography.Title>
      {action}
    </Flex>
  )
}
