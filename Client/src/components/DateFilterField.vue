<script setup lang="ts">
import { numberSearchOperations } from '../consts/searchOperations'
import { useId } from 'vue'
import type { IDateParam } from '@/types/interfaces'

interface IDateFilterProps {
  label: string
}

const uid: string = useId()
const param = defineModel<IDateParam>({ required: true })
defineProps<IDateFilterProps>()
</script>

<template>
  <div class="filter-field">
    <FloatLabel variant="on">
      <DatePicker v-model="param.Value" show-icon fluid :max-date="new Date()" />
      <label :for="uid">{{ label }}</label>
    </FloatLabel>
    <Select
      :id="uid"
      v-model="param.Operation"
      :options="numberSearchOperations"
      option-value="Value"
      option-label="Name"
    />
  </div>
</template>
