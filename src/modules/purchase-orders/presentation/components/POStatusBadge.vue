<script setup lang="ts">
import {
  FileEdit,
  Clock,
  CheckCircle2,
  Truck,
  XCircle,
} from 'lucide-vue-next'
import BaseBadge from '../../../../shared/components/BaseBadge.vue'
import { POStatus, POStatusLabels, POStatusVariants, type POStatusVariant } from '../../domain/enums/POStatus'

defineProps<{ status: POStatus }>()

const iconMap: Record<POStatus, typeof FileEdit> = {
  [POStatus.Draft]: FileEdit,
  [POStatus.PendingBAC]: Clock,
  [POStatus.Approved]: CheckCircle2,
  [POStatus.Delivered]: Truck,
  [POStatus.Cancelled]: XCircle,
}
</script>

<template>
  <BaseBadge :variant="POStatusVariants[status] as POStatusVariant">
    <component :is="iconMap[status]" :size="12" />
    {{ POStatusLabels[status] }}
  </BaseBadge>
</template>
