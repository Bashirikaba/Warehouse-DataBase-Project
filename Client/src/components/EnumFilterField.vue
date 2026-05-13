<script setup lang="ts">
import { useId } from 'vue'
import type { INumberParam } from '@/types/interfaces'
import { RouteTypeEnum } from '@/types/enums'

interface IEnumFilterProps {
  label: string
}

defineProps<IEnumFilterProps>()
const uid: string = useId()
const param = defineModel<INumberParam>({ required: true })
</script>

<template>
  <div class="filter-field">
    <FloatLabel variant="on" style="width: 100%">
      <Select
        style="width: 100%"
        :id="uid"
        v-model="param.Value"
        :options="Object.values(RouteTypeEnum).filter((v) => typeof v == 'number')"
      ></Select>
      <label :for="uid">{{ label }}</label>
    </FloatLabel>
  </div>
</template>
