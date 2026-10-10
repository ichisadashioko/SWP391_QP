import { useEffect, useRef, useState } from 'react'
import { App, Button, Flex, Image, Upload } from 'antd'
import { UploadOutlined } from '@ant-design/icons'
import { IMAGE_MAX_BYTES, IMAGE_TYPES } from '../constants/fieldLength'

interface ImageUploadProps {
  /** New file chosen (Form.Item gives value/onChange). */
  value?: File
  onChange?: (file: File | undefined) => void
  /** Path of the stored image, shown until a new file is chosen. */
  currentUrl?: string
  width?: number
}

/**
 * Image field of SRS III 0.4: preview, Replace and Remove. The file is only kept in
 * the form and sent with Save. Type and size are checked here to answer at once; the
 * API checks them again, including the real file signature (BR-17).
 */
export function ImageUpload({ value, onChange, currentUrl, width = 320 }: ImageUploadProps) {
  const { message } = App.useApp()
  // Preview of the file last chosen here. It only counts while that file is still the
  // field value, so a form reset falls back to the stored image by itself.
  const [preview, setPreview] = useState<{ file: File; url: string }>()
  const lastUrl = useRef<string>(undefined)

  useEffect(() => () => {
    if (lastUrl.current) URL.revokeObjectURL(lastUrl.current)
  }, [])

  const choose = (file: File | undefined) => {
    if (lastUrl.current) URL.revokeObjectURL(lastUrl.current)
    const url = file ? URL.createObjectURL(file) : undefined
    lastUrl.current = url
    setPreview(file && url ? { file, url } : undefined)
    onChange?.(file)
  }

  const shown = value && preview?.file === value ? preview.url : currentUrl

  const picker = (label: string) => (
    <Upload
      accept={IMAGE_TYPES.join(',')}
      showUploadList={false}
      beforeUpload={(file) => {
        if (!IMAGE_TYPES.includes(file.type)) {
          message.error('The image must be a JPG or PNG file.')
        } else if (file.size > IMAGE_MAX_BYTES) {
          message.error('The image must be 2 MB or smaller.')
        } else {
          choose(file)
        }
        return false // never upload on its own; the form sends it on Save
      }}
    >
      <Button icon={<UploadOutlined />}>{label}</Button>
    </Upload>
  )

  return (
    <Flex vertical gap={8} align="start">
      {shown && <Image src={shown} width={width} style={{ objectFit: 'cover', borderRadius: 6 }} />}
      <Flex gap={8}>
        {picker(shown ? 'Replace' : 'Upload image')}
        {value && <Button onClick={() => choose(undefined)}>Remove</Button>}
      </Flex>
    </Flex>
  )
}
